using Grpc.Net.Client;
using Cysharp.Net.Http;

public static class GrpcChannelProvider
{
    private static GrpcChannel _channel;
#if DEBUG
    //private const string ServerURL = "http://localhost:5244";
    private const string ServerURL = "http://vm-bicycle-racing.eastasia.cloudapp.azure.com:5244";
# else
    private const string ServerURL = "http://vm-bicycle-racing.eastasia.cloudapp.azure.com:5244";
# endif
    public static GrpcChannel GetChannel()
    {
        if (_channel != null) return _channel;

        var handler = new YetAnotherHttpHandler
        {
            Http2Only = true,                 // Åö ïKê{
            SkipCertificateVerification = true // äJî≠éûÇÃÇ›
        };
        _channel = GrpcChannel.ForAddress(
            ServerURL,
            new GrpcChannelOptions
            {
                HttpHandler = handler
            }
        );

        return _channel;
    }

    public static void Dispose()
    {
        _channel?.Dispose();
        _channel = null;
    }
}