using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace InstaSharper.Logger
{
    internal class DebugLogger : IInstaLogger
    {
        public void LogRequest(HttpRequestMessage request)
        {
            WriteSeprator();
            Console.WriteLine($"Request: {request.Method} {request.RequestUri}");
            WriteHeaders(request.Headers);
            WriteProperties(request.Properties);
            WriteSeprator();
        }

        public void LogResponse(HttpResponseMessage response)
        {
            Console.WriteLine($"[+] Response: {response.RequestMessage.Method} {response.RequestMessage.RequestUri}");
            WriteContent(response.Content, Formatting.None, 0);
        }

        public void LogException(Exception ex)
        {
            Console.WriteLine($"Exception: {ex}");
            Console.WriteLine($"Stacktrace: {ex.StackTrace}");
        }

        public void LogInfo(string info)
        {
            Console.WriteLine($"Info:\n{info}");
        }

        private void WriteHeaders(HttpHeaders headers)
        {
            if (headers == null) return;
            if (!headers.Any()) return;
            Console.WriteLine("Headers:");
            foreach (var item in headers)
                Console.WriteLine($"{item.Key}:{JsonConvert.SerializeObject(item.Value)}");
        }

        private void WriteProperties(IDictionary<string, object> properties)
        {
            if (properties == null) return;
            if (properties.Count == 0) return;

            Console.WriteLine($"Properties:\n{JsonConvert.SerializeObject(properties, Formatting.Indented)}");
        }

        private async void WriteContent(HttpContent content, Formatting formatting, int maxLen = 0)
        {
            Console.WriteLine("Content:");
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

        private string FormatJson(string json)
        {
            dynamic parsedJson = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
        }
    }
}