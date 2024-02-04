using System;
using System.Net.Http;
using System.Text;

namespace SandboxClient.Model;

public static class FakeRequestExt
{
    public static HttpRequestMessage Convert(this FakeRequest fakeReq, WebServerSetting setting)
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
            string? contentType = null;
            var encoding = Encoding.Default;
            if (fakeReq.Header != null)
            {
                // content-type
                fakeReq.Header.TryGetValue("content-type", out contentType);

                // content-encoding
                fakeReq.Header.TryGetValue("content-encoding", out string? contentEncoding);
                if (contentEncoding != null)
                {
                    encoding = Encoding.GetEncoding(contentEncoding);
                }
            }
            contentType ??= "application/json";
            content = new StringContent(body, encoding, contentType);
        }

        // http content body
        var req = new HttpRequestMessage()
        {
            Method = method,
            RequestUri = fakeReq.BuildUri(setting.Url, setting.Port),
            Content = content,
        };
        if (fakeReq.Header != null)
        {
            foreach (var (k, v) in fakeReq.Header)
            {
                if (!k.StartsWith("content"))
                {
                    req.Headers.Add(k, v);
                }
            }
        }
        return req;
    }
}
