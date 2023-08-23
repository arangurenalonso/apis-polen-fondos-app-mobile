namespace Presentation.Middleware
{
    using Application.Exception;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using Presentation.Errors;
    using System.Net;
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
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
                    case ForeignKeyNotFoundException foreignKeyNotFoundException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, ex.Message, foreignKeyNotFoundException.Errors));
                        break;
                    default:
                        break;
                }


                if (string.IsNullOrEmpty(result))
                {
                    //var detail = "";
                    //if (ex.InnerException != null)
                    //{
                    //    detail = ex.InnerException.Message;
                    //    if (ex.InnerException.InnerException != null)
                    //    {
                    //        detail = detail.Replace(" See the inner exception for details.", "");
                    //        detail += " " + ex.InnerException.InnerException.Message;
                    //    }
                    //}
                    //if (string.IsNullOrEmpty(detail))
                    //{
                    //    detail = ex.StackTrace;
                    //}

                    result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, ex.Message, ex));
                }
                context.Response.StatusCode = statusCode;

                await context.Response.WriteAsync(result);
            }
        }
    }
}
