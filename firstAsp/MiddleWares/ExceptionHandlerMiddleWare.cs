using System.Net;

namespace firstAsp.MiddleWares
{
    public class ExceptionHandlerMiddleWare
    {
        private readonly ILogger<ExceptionHandlerMiddleWare> _logger;
        private readonly RequestDelegate next;
        public ExceptionHandlerMiddleWare(ILogger<ExceptionHandlerMiddleWare> loogger, RequestDelegate next)
        {
            this._logger = loogger;
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext) 
            {
                try 
                    {   
                        await next(httpContext);
                    }
                catch (Exception ex) 
                    {
                 var errorId=Guid.NewGuid().ToString();
                 _logger.LogError(ex,$"{errorId}:{ex.Message}");
                  httpContext.Response.StatusCode=(int) HttpStatusCode.InternalServerError;
                  httpContext.Response.ContentType="application/json";
                var error = new
                {
                    Id = errorId,
                    ErrorMessage="something went wrong "
                };
                await httpContext.Response.WriteAsJsonAsync(error);
                    
                    }
            }
    }
}
