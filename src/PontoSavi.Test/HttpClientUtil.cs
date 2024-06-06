using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

using PontoSavi.Domain.Exceptions;
using PontoSavi.Test.DTOs;

namespace PontoSavi.Test.Utils;

public class HttpClientUtil
{
    public string _accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6ImF0K2p3dCJ9.eyJuYW1laWQiOiIwMUhaTkhBQjBXS1RYNjlKR0NTR0pGSlZEWSIsInVuaXF1ZV9uYW1lIjoiZGV2Iiwicm9sZSI6WyJEZXNlbnZvbHZlZG9yIiwiQ29sYWJvcmFkb3IiXSwibmJmIjoxNzE3NjM2Njg0LCJleHAiOjE3MTgyNDE0ODQsImlhdCI6MTcxNzYzNjY4NCwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MTc4IiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo1MTc4In0.xhPM4Li0y5A4LJ7gCLTNI5S39mYoX40jbW5h3OPGuis";
    public string _refreshToken = string.Empty;

    /// <summary>
    /// Executes a query using the provided HttpClient and filter, and returns the result as an instance of type T.
    /// Note: Pagination and OrderBy are unstable.
    /// </summary>
    public async Task<T> GetFromQuery<T>(HttpClient client, dynamic filter) where T : class
    {
        var query = BuildQuery(filter);
        client = AddAuthorization(client);
        var response = await client.GetAsync($"?{query}");
        return await DeserializeResponse<T>(response);
    }

    /// <summary>
    /// Executes a query using the provided HttpClient and filter, and returns a single result of type T.
    /// </summary>
    /// <returns>A single result of type T.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the query returns no results.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the query returns more than one result.</exception>
    public async Task<T> GetSingleFromQuery<T>(HttpClient client, dynamic filter) where T : class
    {
        List<T> query = await GetFromQuery<List<T>>(client, filter);
        var querySingle = query.Single();
        return querySingle;
    }

    private string BuildQuery(object filter)
    {
        var properties = filter.GetType().GetProperties();
        var queryParameters = new List<string>();

        foreach (var property in properties)
        {
            var value = property.GetValue(filter);

            if (value is null) continue;

            var propertyName = property.Name;
            var encodedValue = Uri.EscapeDataString(value is DateTime date ? date.ToString("yyyy-MM-ddTHH:mm:ss.fffffff") : value.ToString()!);
            queryParameters.Add($"{propertyName}={encodedValue}");
        }

        return string.Join("&", queryParameters);
    }

    public virtual async Task<T> GetFromQueryId<T>(HttpClient client, dynamic id) where T : class
    {
        client = AddAuthorization(client);
        var response = await client.GetAsync($"?Id={id}");
        return await DeserializeResponse<T>(response);
    }

    public virtual async Task<T> GetFromUri<T>(HttpClient client, dynamic obj) where T : class
    {
        client = AddAuthorization(client);
        var response = await client.GetAsync($"{obj}");
        return await DeserializeResponse<T>(response);
    }

    public virtual async Task<int> GetCount(HttpClient client)
    {
        client = AddAuthorization(client);
        var response = await client.GetAsync("count");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        return await DeserializeResponse<int>(response);
    }

    public virtual async Task<T> Get<T>(HttpClient client) where T : class
    {
        client = AddAuthorization(client);
        var response = await client.GetAsync("");
        return await DeserializeResponse<T>(response);
    }

    public virtual async Task<T> PostFromBody<T>(HttpClient client, dynamic obj) where T : class
    {
        client = AddAuthorization(client);
        var objJson = JsonContent.Create(obj);
        var response = await client.PostAsync("", objJson);
        return await DeserializeResponse<T>(response);
    }

    public virtual async Task<T> PostFromForm<T>(HttpClient client, dynamic obj) where T : class
    {
        client = AddAuthorization(client);
        var formData = new MultipartFormDataContent();
        formData = await FormContent(formData, obj);
        var response = await client.PostAsync("", formData);
        return await DeserializeResponse<T>(response);
    }

    public virtual async Task<MultipartFormDataContent> FormContent(MultipartFormDataContent formData, dynamic obj, string? entityName = null)
    {
        foreach (var property in obj.GetType().GetProperties())
        {
            if (property == null || property!.GetValue(obj) == null) continue;

            if (property!.PropertyType.Name == "IFormFile")
            {
                var formFile = property.GetValue(obj) as IFormFile ?? throw new Exception("FormFile is null");
                formData.Add(new StreamContent(formFile.OpenReadStream()), !entityName.IsNullOrEmpty() ? entityName + "." + property.Name : property.Name, formFile.FileName);
                continue;
            }

            if (property!.PropertyType.Namespace == "PontoSavi.Domain.Entities")
                formData = await FormContent(formData, property.GetValue(obj), property.Name);
            else
                formData.Add(new StringContent(property.GetValue(obj).ToString()), !entityName.IsNullOrEmpty() ? entityName + "." + property.Name : property.Name);
        }

        return formData;
    }

    public virtual async Task<T> PutFromBody<T>(HttpClient client, dynamic obj) where T : class
    {
        client = AddAuthorization(client);
        var objJson = JsonContent.Create(obj);
        var response = await client.PutAsync("", objJson);
        return await DeserializeResponse<T>(response);
    }

    public async Task<T> DeleteFromUri<T>(HttpClient client, dynamic id) where T : class
    {
        client = AddAuthorization(client);
        var response = await client.DeleteAsync($"{id}");
        return await DeserializeResponse<T>(response);
    }

    public async Task<T> DeleteFromBody<T>(HttpClient client, dynamic obj) where T : class
    {
        client = AddAuthorization(client);
        var objJson = JsonContent.Create(obj);
        var request = new HttpRequestMessage(HttpMethod.Delete, "");
        request.Content = objJson;
        var response = await client.SendAsync(request);
        return await DeserializeResponse<T>(response);
    }

    private async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
    {
        if (typeof(T) == typeof(AppHttpResponse))
            return (T)(object)new AppHttpResponse(response);

        var content = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.OK || typeof(T) == typeof(AppException))
            return JsonConvert.DeserializeObject<T>(content) ??
                throw new Exception("Deserialized object is null");

        var errorMessage = $"Unexpected response status code:\n" +
                           $"Content:\n{content}\n" +
                           $"StatusCode:\n{response.StatusCode}\n" +
                           $"Resquest:\n{response.RequestMessage}\n" +
                           $"Response:\n{response}\n" +
                           $"AbsoluteUri:\n{response!.RequestMessage!.RequestUri!.AbsoluteUri}";

        throw new Exception(errorMessage);
    }

    private HttpClient AddAuthorization(HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
        return client;
    }
}
