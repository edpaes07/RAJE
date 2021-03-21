using Raje.DL.Request.Admin;
using Raje.DL.Response.Adm;
using Raje.DL.Services.BLL.Admin;
using BrotliSharpLib;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raje.BLL.Services.Admin
{
    public class LoggingMiddlewareService : ILoggingMiddlewareService
    {
        private readonly RequestDelegate _next;

        public LoggingMiddlewareService(RequestDelegate next)
        {
            _next = next;
        }

        public object ApiRequest { get; private set; }

        public async Task Invoke(HttpContext context, ILogService _service)
        {
            //First, get the incoming request
            var request = await FormatRequest(context.Request);

            //Copy a pointer to the original response body stream
            var originalBodyStream = context.Response.Body;

            LogMiddlewareResponse response;
            //Create a new memory stream...
            using (var responseBody = new MemoryStream())
            {
                //...and use that for the temporary response body
                context.Response.Body = responseBody;

                //Continue down the Middleware pipeline, eventually returning to this class
                await _next(context);

                //Format the response from the server
                response = FormatResponse(context.Response);

                //Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
                await responseBody.CopyToAsync(originalBodyStream);
            }

            // If request was in swagger html, do nothing.
            if (String.IsNullOrWhiteSpace(request.Api))
                return;

            // IF request was to login
            if (request.Api.Contains("UserName"))
                request.Request = "";

            #region [Save Log]
            // Instantiating HaasLogRequest to save values in DB
            LogRequest logRequest = new LogRequest();

            logRequest.Api = request.Api;
            logRequest.UrlQuery = request.UrlQuery;
            logRequest.Method = request.Method;
            logRequest.Request = request.Request;

            logRequest.Input = true; // TODO: a primeiro momento vamos passar o input como true, futuramente implementar lógica que será true somente se a chamada for feita pelo sistema Raje

            logRequest.Code = response.Code;
            logRequest.Response = response.Response;

            // Save log
            if (!request.UrlQuery.Contains("Search") && !request.UrlQuery.Contains("Export") && !request.Method.Contains("GET"))
                _service.Create(logRequest);
            #endregion
        }

        public async Task<LogMiddlewareRequest> FormatRequest(HttpRequest request)
        {
            //This line allows us to set the reader for the request back at the beginning of its stream.
            request.EnableBuffering();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];

            await request.Body.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);

            var bodyAsText = Encoding.UTF8.GetString(buffer);

            request.Body.Position = 0;

            string path = request.Path.Value;

            if (path.Split('/').Length >= 4)
                path = path.Split('/')[3];
            else
                path = "";

            var data = new LogMiddlewareRequest { Api = path, Request = bodyAsText, UrlQuery = $"{request.Host}{request.Path}{request.QueryString}", Method = request.Method };

            return data;
        }

        public LogMiddlewareResponse FormatResponse(HttpResponse response)
        {

            //We need to read the response stream from the beginning...
            response.Body.Seek(0, SeekOrigin.Begin);
            var buffer = ReadFully(response.Body);

            //decompress if necessary
            StringValues encoding;
            if (response.Headers.TryGetValue("Content-Encoding", out encoding) && encoding.First() == "br")
            {
                buffer = Brotli.DecompressBuffer(buffer, 0, buffer.Length);
            }

            //...and copy it into a string
            string text = System.Text.Encoding.UTF8.GetString(buffer, 0, buffer.Length);

            //We need to reset the reader for the response so that the client can read it.
            response.Body.Seek(0, SeekOrigin.Begin);


            var data = new LogMiddlewareResponse { Code = (short)(response.StatusCode), Response = text };
            //Return the string for the response, including the status code (e.g. 200, 404, 401, etc.)
            return data;
        }

        public byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}