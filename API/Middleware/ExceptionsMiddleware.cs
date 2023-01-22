using System.Net;
using System.Text.Json;
using Application.Core;

namespace API.Middleware
{
    public class ExceptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionsMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionsMiddleware(RequestDelegate next,ILogger<ExceptionsMiddleware> logger,IHostEnvironment env)
        {
            _env = env; //To check which environment running in
            _logger = logger; //To log the logic in middleware
            _next = next; //To continue step of middleware
            
        }

        public async Task InvokeAsync(HttpContext context) 
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex,ex.Message);
                context.Response.ContentType="application/json"; 
                context.Response.StatusCode= (int)HttpStatusCode.InternalServerError; //Response error 500

                var response = _env.IsDevelopment() 
                ? new AppException(context.Response.StatusCode,ex.Message,ex.StackTrace?.ToString())
                : new AppException(context.Response.StatusCode , "Internal Server Error");

                var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

                var json=JsonSerializer.Serialize(response,options);//JSON Response

                await context.Response.WriteAsync(json);
            }
        }
    }
        
    
}
//Note
//In Catch(Exception ex) need to be specify application/json because the context is outside of API Controllers.
//By Default of API controllers is returning responses as appliaction/json
//In var options need to set the format of JSON because outside of API Controllers 
//By default in API Controllers the format json is like in var options