using Concordium.Sdk.Exceptions;

namespace Concordium.Sdk.Types;

/// <summary>
/// Data generated as part of initializing a single contract instance.
/// </summary>
/// <param name="ContractVersion">Contract version.</param>
/// <param name="ModuleReference">A reference to a smart contract module deployed on the chain.</param>
/// <param name="ContractAddress">Represent the contract address of the newly initialized contract instance.</param>
/// <param name="Amount">Represent the CCD amount the contract instance was initialized with.</param>
/// <param name="InitName">The name of the contract.</param>
/// <param name="Events">A list of events generated by a smart contract.</param>
public sealed record ContractInitializedEvent(
    ContractVersion ContractVersion,
    ModuleReference ModuleReference,
    ContractAddress ContractAddress,
    CcdAmount Amount,
    ContractName InitName,
    IList<ContractEvent> Events
    )
{
    /// <summary>
    /// Create a instance of a Contract Initialization Event
    /// </summary>
    /// <param name="initializedEvent">initialization event</param>
    /// <returns>Contract Initialization Event</returns>
    /// <exception cref="MissingEnumException{ContractVersion}">When contract version not known</exception>
    internal static ContractInitializedEvent From(Grpc.V2.ContractInitializedEvent initializedEvent)
    {
        var contractVersion = initializedEvent.ContractVersion.Into();
        var moduleReference = new ModuleReference(initializedEvent.OriginRef.Value);
        var contractAddress = ContractAddress.From(initializedEvent.Address);
        var amount = CcdAmount.FromMicroCcd(initializedEvent.Amount.Value);
        var initName = ContractName.From(initializedEvent.InitName);
        var events = initializedEvent.Events
            .Select(e => new ContractEvent(e.Value.ToByteArray()))
            .ToList();
        return new ContractInitializedEvent(contractVersion, moduleReference, contractAddress, amount,
            initName, events);
    }
}
