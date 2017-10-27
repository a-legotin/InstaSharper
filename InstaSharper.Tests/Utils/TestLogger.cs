using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace InstaSharper.Tests.Utils
{
    internal class TestLogger : ILogger
    {
        public void OnRequest(object request)
        {
            WriteSeprator();
            if (request is HttpRequestMessage req)
            {
                Console.WriteLine($"[+] Request: {req.Method} {req.RequestUri}");
                WriteHeaders(req.Headers);
                WriteProperties(req.Properties);
            }
            else if (request is Uri uri)
            {
                Console.WriteLine($"[+] Request: {HttpMethod.Get} {uri}");
            }
            else
            {
                Console.WriteLine($"Request[{request.GetType()}]");
            }
            //WriteObject(request);
            WriteSeprator();
        }

        public void OnResponse(object response)
        {
            if (response is HttpResponseMessage rsp)
            {
                Console.WriteLine($"[+] Response: {rsp.RequestMessage.Method} {rsp.RequestMessage.RequestUri}");
                WriteContent(rsp.Content, Formatting.None, 0);
            }
            //WriteObject(response);
        }

        public void OnError(Exception ex)
        {
            Console.WriteLine("[+] Error:");
            Console.WriteLine(ex.ToString());
        }

        public void OnInfo(string info)
        {
            Console.WriteLine($"[+] Info:\n{info}");
        }

        private void WriteHeaders(HttpHeaders headers)
        {
            if (headers == null) return;
            if (!headers.Any()) return;
            Console.WriteLine("[+] Headers:");
            foreach (var item in headers)
                Console.WriteLine($"{item.Key}:{JsonConvert.SerializeObject(item.Value)}");
        }

        private void WriteProperties(IDictionary<string, object> properties)
        {
            if (properties == null) return;
            if (properties.Count == 0) return;

            Console.WriteLine($"[+] Properties:\n{JsonConvert.SerializeObject(properties, Formatting.Indented)}");
        }

        private async void WriteContent(HttpContent content, Formatting formatting, int maxLen = 0)
        {
            Console.WriteLine("[+] Content:");
            var raw = await content.ReadAsStringAsync();
            if (formatting == Formatting.Indented) raw = FormatJson(raw);
            raw = raw.Contains("<!DOCTYPE html>") ? "got html content!" : raw;
            if ((raw.Length > maxLen) & (maxLen != 0))
                raw = raw.Substring(0, maxLen);
            Console.WriteLine(raw);
        }

        private void WriteSeprator()
        {
            var sep = new StringBuilder();
            for (var i = 0; i < 100; i++) sep.Append("-");
            Console.WriteLine(sep);
        }

        private void WriteObject(object obj)
        {
            Console.WriteLine(JsonConvert.SerializeObject(obj));
        }

        private string FormatJson(string json)
        {
            dynamic parsedJson = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
        }
    }
}