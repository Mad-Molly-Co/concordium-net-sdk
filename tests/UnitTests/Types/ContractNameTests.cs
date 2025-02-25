using Concordium.Sdk.Types;
using FluentAssertions;
using Xunit;

namespace Concordium.Sdk.Tests.UnitTests.Types;

public class ContractNameTests
{
    [Theory]
    [InlineData("init_contract", true, null)] // Valid name
    [InlineData("init_contract.", false, ContractName.ValidationError.ContainsDot)] // Contains '.'
    [InlineData("init_contract_with_a_very_long_name_that_exceeds_the_maximum_allowed_lengthcontract_with_a_very_long_name_that_exceeds_the_maximum_allowed_length", false, ContractName.ValidationError.TooLong)] // Exceeds maximum length
    [InlineData("init_contract$", true, null)] // Contains ASCII punctuation
    [InlineData("init_contract💡", false, ContractName.ValidationError.InvalidCharacters)] // Contains non-ASCII character
    [InlineData("init_contract ", false, ContractName.ValidationError.InvalidCharacters)] // Contains whitespace
    [InlineData("init_ contract", false, ContractName.ValidationError.InvalidCharacters)] // Contains whitespace
    [InlineData("contract", false, ContractName.ValidationError.MissingInitPrefix)] // Does not start with 'init_'
    public void WhenCallingTryParse_ValidatesAndParsesReceiveName(
        string name,
        bool expectedResult,
            ContractName.ValidationError? expectedError)
    {
        // Act
        var result = ContractName.TryParse(name, out var output);
        var (initName, error) = output;

        // Assert
        result.Should().Be(expectedResult);
        if (expectedResult)
        {
            initName.Should().NotBeNull();
            initName!.Name.Should().Be(name);
            error.Should().BeNull();
        }
        else
        {
            error.Should().NotBeNull();
            error!.Should().Be(expectedError);
            initName.Should().BeNull();
        }
    }
}
