namespace FallenClient.Library.Models.Http;

public record ApiGatewayResponse(
    int StatusCode = 200,
    Dictionary<string, string>? Headers = null,
    string Body = "",
    bool IsBase64Encoded = false
);