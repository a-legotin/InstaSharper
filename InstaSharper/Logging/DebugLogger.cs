using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using InstaSharper.Abstractions.Logging;
using InstaSharper.Abstractions.Serialization;

namespace InstaSharper.Logging;

internal class DebugLogger : ILogger
{
    private readonly LogLevel _logLevel;
    private readonly IJsonSerializer _serializer;

    public DebugLogger(LogLevel loglevel,
                       IJsonSerializer serializer)
    {
        _logLevel = loglevel;
        _serializer = serializer;
    }

    public Task LogRequest(HttpRequestMessage request)
    {
        if (_logLevel < LogLevel.Request)
            return Task.CompletedTask;
        return Task.Run(() =>
        {
            WriteSeparator();
            Write($"Request: {request.Method} {request.RequestUri}");
            WriteHeaders(request.Headers);
            WriteProperties(request.Options);
        });
    }

    public void LogRequest(Uri uri)
    {
        if (_logLevel < LogLevel.Request) return;
        Write($"Request: {uri}");
    }

    public Task LogResponse(HttpResponseMessage response)
    {
        if (_logLevel < LogLevel.Response)
            return Task.CompletedTask;
        return Task.Run(async () =>
        {
            Write($"Response: {response.RequestMessage.Method} {response.RequestMessage.RequestUri}");
            await WriteContent(response.Content);
        });
    }

    public void LogException(Exception ex)
    {
        if (_logLevel < LogLevel.Exceptions)
            return;
        Console.WriteLine($"Exception: {ex}");
        Console.WriteLine($"Stacktrace: {ex.StackTrace}");
    }

    public void LogInfo(string info)
    {
        if (_logLevel < LogLevel.Info)
            return;
        Write($"Info:{Environment.NewLine}{info}");
    }

    public void LogError(string info)
    {
        if (_logLevel < LogLevel.Error)
            return;
        Write($"Error:{Environment.NewLine}{info}");
    }

    private void WriteHeaders(HttpHeaders headers)
    {
        if (headers == null) return;
        if (!headers.Any()) return;
        Write("Headers:");
        foreach (var item in headers)
            Write($"{item.Key}:{_serializer.Serialize(item.Value)}");
    }

    private void WriteProperties(IDictionary<string, object> properties)
    {
        if (properties == null) return;
        if (properties.Count == 0) return;
        Write($"Properties:\n{_serializer.SerializeIndented(properties)}");
    }

    private async Task WriteContent(HttpContent content,
                                    int maxLength = 0)
    {
        Write("Content:");
        var raw = await content.ReadAsStringAsync();
        raw = raw.Contains("<!DOCTYPE html>") ? "got html content!" : raw;
        if ((raw.Length > maxLength) & (maxLength != 0))
            raw = raw.Substring(0, maxLength);
        Write(raw);
    }

    private void WriteSeparator()
    {
        var sep = new StringBuilder();
        for (var i = 0; i < 100; i++) sep.Append("-");
        Write(sep.ToString());
    }


    private void Write(string message)
    {
        Debug.WriteLine($"{DateTime.Now.ToShortTimeString()}:\t{message}");
    }
}