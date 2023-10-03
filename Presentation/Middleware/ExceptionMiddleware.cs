namespace Presentation.Middleware
{
    using Application.Contracts.Repositories.Base;
    using Application.Exception;
    using Domain.Entities;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using Presentation.Errors;
    using Presentation.Middleware.Models;
    using System.Diagnostics;
    using System.Net;
    using System.Text;

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var unitOfWork = (IUnitOfWork)context.RequestServices.GetService(typeof(IUnitOfWork));

            MemoryStream newResponseBody = new MemoryStream();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var log = new RequestResponseLog();

            var originalResponseBody = context.Response.Body;
            try
            {
                log.RequestMethod = context.Request.Method;
                log.RequestUrl = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}";
                log.RequestHeaders = JsonConvert.SerializeObject(context.Request.Headers);
                log.RequestBody = await FormatRequest(context.Request);

                context.Response.Body = newResponseBody;

                await _next(context);
                context.Response.Body = originalResponseBody;
                newResponseBody.Seek(0, SeekOrigin.Begin);
                await newResponseBody.CopyToAsync(originalResponseBody);

                log.ResponseBody = await FormatResponse(newResponseBody, context.Response);

            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var statusCode = (int)HttpStatusCode.InternalServerError;
                var result = string.Empty;
                switch (ex)
                {
                    case NotFoundException notFoundException:
                        statusCode = (int)HttpStatusCode.NotFound;
                        result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, ex.Message, notFoundException.Message.ToString()));
                        break;
                    case CustomValidationException validationException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, ex.Message, validationException.Errors));
                        break;
                    case AuthorizationException autorizationException:
                        statusCode = (int)HttpStatusCode.Unauthorized;
                        result = JsonConvert.SerializeObject(new CodeErrorResponse(statusCode, autorizationException.Message));
                        break;
                    case ConsumoApisInternasException consumoApisInternasException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, consumoApisInternasException.Mensaje, consumoApisInternasException.Details));
                        break;
                    default:
                        result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, ex.Message, ex));
                        break;
                }
                log.ResponseBody = result;
                context.Response.Body = originalResponseBody;
                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsync(result);
            }
            finally
            {
                stopwatch.Stop();

                log.Timestamp = DateTime.UtcNow;
                log.ResponseStatus = context.Response.StatusCode;
                log.ResponseHeaders = JsonConvert.SerializeObject(context.Response.Headers);
                log.UserAgent = context.Request.Headers["User-Agent"];
                log.ElapsedTime = stopwatch.Elapsed;

                var repository = unitOfWork.Repository<RequestResponseLog>();
                await repository.AddAsync(log);
                await unitOfWork.Complete();
                if (newResponseBody != null)
                {
                    newResponseBody.Close();
                    newResponseBody.Dispose();
                }
            }
        }
        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableBuffering();

            using var reader = new StreamReader(request.Body, encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
            var bodyStr = await reader.ReadToEndAsync();
            request.Body.Position = 0;

            var requestInfo = new RequestInfo
            {
                Method = request.Method,
                Scheme = request.Scheme,
                Host = request.Host.ToString(),
                Path = request.Path,
                QueryString = request.QueryString.ToString(),
            };

            var contentType = request.ContentType;

            if (contentType != null && contentType.StartsWith("multipart/form-data"))
            {
                var files = new List<FileDetail>();
                var formReader = request.ReadFormAsync();
                foreach (var file in formReader.Result.Files)
                {
                    var fileDetail = new FileDetail
                    {
                        FileName = file.FileName,
                        FileSize = file.Length,
                        ContentType = file.ContentType
                    };
                    files.Add(fileDetail);
                }
                requestInfo.Body = files;
            }
            else if (contentType != null && contentType.StartsWith("application/json"))
            {
                object bodyObj = null;
                try
                {
                    bodyObj = JsonConvert.DeserializeObject<object>(bodyStr);
                }
                catch (JsonException ex)
                {
                    bodyObj = bodyStr;
                }
                requestInfo.Body = bodyObj;
            }
            else
            {
                requestInfo.Body = "No definido";
            }
            return JsonConvert.SerializeObject(requestInfo);
        }
        private async Task<string> FormatResponse(MemoryStream responseMemoryStream, HttpResponse response)
        {
            var contentType = response.ContentType;

            if (contentType == null) 
            {
                return "Content type is null"; // or whatever logic you need
            }
            if (contentType.StartsWith("text"))
            {
                responseMemoryStream.Seek(0, SeekOrigin.Begin);
                var reader = new StreamReader(responseMemoryStream);
                var text = await reader.ReadToEndAsync();
                responseMemoryStream.Seek(0, SeekOrigin.Begin);

                var responseInfo = new ResponseInfo
                {
                    ContentType = contentType,
                    TextContent = text,
                    Size = text.Length
                };

                return JsonConvert.SerializeObject(responseInfo);
            }
            else if (contentType.StartsWith("application/json"))
            {
                responseMemoryStream.Seek(0, SeekOrigin.Begin);
                var reader = new StreamReader(responseMemoryStream);
                var jsonText = await reader.ReadToEndAsync();
                responseMemoryStream.Seek(0, SeekOrigin.Begin);
                return jsonText;
            }
            else
            {
                var responseInfo = new ResponseInfo();
                responseInfo.ContentType = contentType;
                string contentDispositionHeader = response.Headers["Content-Disposition"].ToString();
                string fileName = "No especificado en el Content-Disposition";
                if (!string.IsNullOrEmpty(contentDispositionHeader))
                {
                    fileName = contentDispositionHeader;
                }
                responseInfo.TextContent = "Archivo";
                responseInfo.FileName = fileName;
                responseInfo.Size = responseMemoryStream.Length;
                return JsonConvert.SerializeObject(responseInfo);
            }

        }
    }
}
