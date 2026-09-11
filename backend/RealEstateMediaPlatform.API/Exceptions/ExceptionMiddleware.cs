using RealEstateMediaPlatform.API.Common;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Azure;
using RealEstateMediaPlatform.API.Exceptions;

namespace RealEstateMediaPlatform.API.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next; //RequestDelegate 表示一个“处理 HTTP 请求的方法”。
        private readonly ILogger<ExceptionMiddleware> _logger; //ILogger<T> 是 .NET 提供的日志记录接口，T 是日志记录器的类别，通常是当前类的类型。

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context) 
        // Invoke 或者 InvodeAsync是 .net约定中间件应该提供的方法命名
        // HttpContext context 代表当前HTTP的请求和响应。例如context.Response.StatusCode = 404
        // _next(context) 把当前 context 传给后面的请求管道。如果删除，后面的中间件和 Controller 不会执行，请求可能得到空响应。
        {
            try
            {
                await _next(context);
            }
            // try里会一层层尝试执行，直到抓住错误就会放到catch继续
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");
                await HandleExceptionAsync(context, ex);
            }
        }

            // exception
            // → 告诉方法“发生了什么错误”

            // exception 是需要处理的错误，context. Response 是把处理结果送回前端的出口。

            // context
            // → 让方法控制“这次请求应该怎样回复”
            
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = exception switch
            {
                ArgumentException argumentEx => ApiResponse<object>.Fail(
                 argumentEx.Message,
                 "VALIDATION_ERROR"
                ),
                KeyNotFoundException keyNotFoundEx => ApiResponse<object>.Fail(
                    keyNotFoundEx.Message,
                    "NOT_FOUND"
                ),
                UnauthorizedAccessException unauthorizedEx => ApiResponse<object>.Fail(
                    unauthorizedEx.Message,
                    "UNAUTHORIZED"
                ),
                InvalidOperationException invalidOpEx => ApiResponse<object>.Fail(
                    invalidOpEx.Message,
                    "INVALID_OPERATION"),
                Azure.RequestFailedException azEx => ApiResponse<object>.Fail(
                  "Upload Failed",
                  "UPLOAD_ERROR"
                 ),

                FeatureNotAvailableException featureEx => ApiResponse<object>.Fail(
                   featureEx.Message,
                   "FEATURE_NOT_AVAILABLE"
                ),
                _ => ApiResponse<object>.Fail(
                    exception.Message,
                    "INTERNAL_ERROR"
                )
            };

            context.Response.StatusCode = GetStatusCode(exception);
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private static int GetStatusCode(Exception exception) => exception switch
        {
            ArgumentException => 400,    // Bad Request - Validation error    
            KeyNotFoundException => 404, // Not Found - Resource not found
            UnauthorizedAccessException => 401, // Unauthorized - Authentication required
            InvalidOperationException => 400,  // Bad Request - Invalid operation/business rule violation
            Azure.RequestFailedException azEx => azEx.Status,//Upload Failed
            FeatureNotAvailableException=> 400, // Feature Not Available
            _ => 500 // Internal Server Error - Unknown error
        };

    };
    }
