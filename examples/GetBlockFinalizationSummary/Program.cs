using CommandLine;
using Concordium.Sdk.Client;
using Concordium.Sdk.Types;

// We disable these warnings since CommandLine needs to set properties in options
// but we don't want to give default values.
#pragma warning disable CS8618

namespace GetBlockFinalizationSummary;

internal sealed class GetBlockFinalizationSummaryOptions
{
    [Option(HelpText = "URL representing the endpoint where the gRPC V2 API is served.",
        Default = "http://node.testnet.concordium.com:20000/")]
    public string Endpoint { get; set; }
}


public static class Program
{
    /// <summary>
    /// Example how to use <see cref="ConcordiumClient.GetBlockFinalizationSummaryAsync"/>
    /// </summary>s
    public static async Task Main(string[] args) =>
        await Parser.Default
            .ParseArguments<GetBlockFinalizationSummaryOptions>(args)
            .WithParsedAsync(Run);

    private static async Task Run(GetBlockFinalizationSummaryOptions options)
    {
        using var client = new ConcordiumClient(new Uri(options.Endpoint), new ConcordiumClientOptions());

        var idx = 0UL;
        var awaitResult = true;
        while (awaitResult)
        {
            var blockHeight = new Absolute(idx);
            var response = await client.GetBlockFinalizationSummaryAsync(blockHeight);
            var finalizationSummary = response.Response;
            if (finalizationSummary == null)
            {
                idx++;
                continue;
            }
            awaitResult = false;

            Console.WriteLine($"At height {idx} block {finalizationSummary.BlockPointer} had finalization summary");
            Console.WriteLine($"Finalization round index: {finalizationSummary.Index}, finalization delay: {finalizationSummary.Delay}");
            Console.WriteLine($"With finalizers");
            foreach (var party in finalizationSummary.Finalizers)
            {
                Console.WriteLine($"Baker: {party.BakerId}, weight in committee: {party.Weight} with signature present: {party.SignaturePresent}");
            }
        }
    }
}
