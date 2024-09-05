using System.Net;
using System.Text;
using System.Text.Json;
using FallenClient.Library.Models.Http;
using Library.Operations.Errors;
using Library.Operations.Outcomes;
using static Library.Operations.Extensions.Outcome;

namespace FallenClient.Library.Clients;

public class FallenApi(
    HttpClient client
)
{
    private HttpClient Client { get; } = client;

    public Task<Outcome<TOut>> Get<TOut>(
        string endpoint,
        CancellationToken token = default
    ) => MakeRequest<TOut>(HttpMethod.Get, endpoint, token);
    
    public Task<Outcome<TOut>> Post<TIn, TOut>(
        string endpoint,
        TIn body,
        CancellationToken token = default
    ) => MakeRequest<TIn, TOut>(HttpMethod.Post, endpoint, body, token);
    
    public Task<Outcome<TOut>> Put<TIn, TOut>(
        string endpoint,
        TIn body,
        CancellationToken token = default
    ) => MakeRequest<TIn, TOut>(HttpMethod.Put, endpoint, body, token);
    
    public Task<Outcome<TOut>> Delete<TOut>(
        string endpoint,
        CancellationToken token = default
    ) => MakeRequest<TOut>(HttpMethod.Delete, endpoint, token);

    public Task<Outcome<string>> MakeRequest(
        HttpMethod method,
        string endpoint,
        CancellationToken token = default
    ) => MakeRequest(method, endpoint, null, token);

    public Task<Outcome<string>> MakeRequest<T>(
        HttpMethod method,
        string endpoint,
        T? body,
        CancellationToken token = default
    ) => MakeRequest(
        method,
        endpoint,
        JsonSerializer.Serialize(body),
        token
    );

    public Task<Outcome<TOut>> MakeRequest<TOut>(
        HttpMethod method,
        string endpoint,
        CancellationToken token = default
    ) => MakeRequest<object, TOut>(
        method, endpoint, null, token
    );

    public Task<Outcome<TOut>> MakeRequest<TIn, TOut>(
        HttpMethod method,
        string endpoint,
        TIn? body,
        CancellationToken token = default
    ) => MakeRequest(
        method,
        endpoint,
        JsonSerializer.Serialize(body),
        token
    ).Next(json => Success(
            JsonSerializer.Deserialize<TOut>(json)
        )
    ).Next(obj => obj == null
        ? Failure<TOut>(new IsNull<TOut>())
        : Success(obj)
    );

    public async Task<Outcome<string>> MakeRequest(
        HttpMethod method,
        string endpoint,
        string? body,
        CancellationToken token = default
    )
    {
        var request = new HttpRequestMessage(method, endpoint);

        if (body != null && !body.Equals("null", StringComparison.InvariantCultureIgnoreCase))
        {
            request.Content = new StringContent(
                body, Encoding.UTF8, "application/json"
            );
        }

        try
        {
            var response = await Client.SendAsync(request, token);
            if (!response.IsSuccessStatusCode)
                return Failure<string>(new HttpError(
                    endpoint, response.StatusCode
                ));

            var content = await response.Content.ReadAsStringAsync(token);

            var gatewayResponse = JsonSerializer
                .Deserialize<ApiGatewayResponse>(content);

            if (gatewayResponse == null)
                return Failure<string>(new IsNull<ApiGatewayResponse>("Could not understand response: " + content));

            bool IsSuccess(int code) => code is >= 200 and <= 299;

            if (IsSuccess(gatewayResponse.StatusCode))
                return Success(gatewayResponse.Body);


            return Failure<string>(new HttpError(
                endpoint, (HttpStatusCode)gatewayResponse.StatusCode
            ));
        }
        catch (Exception e)
        {
            return Failure<string>(new CriticalError(e.Message));
        }
    }
}

public class HttpError(
    string endpoint,
    HttpStatusCode responseStatusCode
) : Error(
    $"Request for '{endpoint}' failed with status code {responseStatusCode}"
);