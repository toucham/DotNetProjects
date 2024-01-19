using System;
using System.Net.Http;
using System.Text;

namespace SandboxClient.Model;

public static class FakeRequestExt
{
    public static HttpRequestMessage Convert(this FakeRequest fakeReq, string baseUrl)
    {
        // http method
        var method = HttpMethod.Get;
        switch (fakeReq.Method.ToLower())
        {
            case "post":
                method = HttpMethod.Post;
                break;
            case "delete":
                method = HttpMethod.Delete;
                break;
            case "put":
                method = HttpMethod.Put;
                break;
            case "get":
                method = HttpMethod.Get;
                break;
            default:
                break;
        }

        // http content
        HttpContent? content = null;
        var body = fakeReq.ToString();
        if ((method == HttpMethod.Post || method == HttpMethod.Put) && !string.IsNullOrEmpty(body))
        {
            // content-type
            fakeReq.Header.TryGetValue("content-type", out string? contentType);
            contentType ??= "application/json";

            // content-encoding
            var encoding = Encoding.Default;
            fakeReq.Header.TryGetValue("content-encoding", out string? contentEncoding);
            if (contentEncoding != null)
            {
                encoding = Encoding.GetEncoding(contentEncoding);
            }
            content = new StringContent(body, encoding, contentType);
        }

        // http content body
        var req = new HttpRequestMessage()
        {
            Method = method,
            RequestUri = new Uri(fakeReq.Url ?? baseUrl),
            Content = content,
        };
        foreach (var (k, v) in fakeReq.Header)
        {
            if (!k.StartsWith("content"))
            {
                req.Headers.Add(k, v);
            }
        }
        return req;
    }
}
