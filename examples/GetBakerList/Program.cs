using CommandLine;
using Concordium.Sdk.Client;
using Concordium.Sdk.Types;

// We disable these warnings since CommandLine needs to set properties in options
// but we don't want to give default values.
#pragma warning disable CS8618

namespace GetBakerList;

internal sealed class GetBakerListOptions
{
    [Option(HelpText = "URL representing the endpoint where the gRPC V2 API is served.",
        Default = "http://node.testnet.concordium.com:20000/")]
    public string Endpoint { get; set; }
}


public static class Program
{
    /// <summary>
    /// Example how to use <see cref="ConcordiumClient.GetBakerListAsync"/>
    /// </summary>s
    public static async Task Main(string[] args) =>
        await Parser.Default
            .ParseArguments<GetBakerListOptions>(args)
            .WithParsedAsync(Run);

    private static async Task Run(GetBakerListOptions options)
    {
        using var client = new ConcordiumClient(new Uri(options.Endpoint), new ConcordiumClientOptions());

        var bakers = await client.GetBakerListAsync(new LastFinal());

        Console.WriteLine($"BlockHash: {bakers.BlockHash}");
        await foreach (var baker in bakers.Response)
        {
            Console.WriteLine($"Id: {baker}");
        }
    }
}
