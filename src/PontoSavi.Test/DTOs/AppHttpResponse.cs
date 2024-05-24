using System.Net;

namespace PontoSavi.Test.DTOs;

public class AppHttpResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccessStatusCode { get; set; }
    public string Message { get; set; } = string.Empty;

    public AppHttpResponse() { }

    public AppHttpResponse(HttpResponseMessage httpResponse)
    {
        StatusCode = httpResponse.StatusCode;
        IsSuccessStatusCode = httpResponse.IsSuccessStatusCode;
        Message = httpResponse.Content.ReadAsStringAsync().Result;
    }
}
