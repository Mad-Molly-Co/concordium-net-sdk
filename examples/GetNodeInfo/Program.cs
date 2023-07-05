using CommandLine;
using Concordium.Sdk.Client;


namespace GetNodeInfo;

internal sealed class GetNodeInfoOptions
{
    [Option(HelpText = "URL representing the endpoint where the gRPC V2 API is served.", Required = true,
        Default = "http://node.testnet.concordium.com:20000/")]
    public Uri Uri { get; set; }
}


public static class Program
{
    /// <summary>
    /// Example how to use <see cref="ConcordiumClient.GetNodeInfoAsync"/>
    /// </summary>s
    public static async Task Main(string[] args) =>
        await Parser.Default
            .ParseArguments<GetNodeInfoOptions>(args)
            .WithParsedAsync(Run);

    private static async Task Run(GetNodeInfoOptions options)
    {
        var clientOptions = new ConcordiumClientOptions
        {
            Endpoint = options.Uri
        };
        using var client = new ConcordiumClient(clientOptions);

        var peerVersion = await client.GetNodeInfoAsync();

        Console.WriteLine($"Version of node was: {peerVersion.Version}");
    }
}
