using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;

namespace JEX.Assessment.Application.V1.Behavior.Filters;
public class CatchAllExceptionFilter( IHostEnvironment env ) : IExceptionFilter
{
    public void OnException( ExceptionContext ctx )
    {
        if ( env.IsDevelopment() )
        {
            ctx.Result = new ContentResult {
                ContentType = "text/html",
                Content = ctx.Exception.ToString(),
                StatusCode = StatusCodes.Status400BadRequest
            };
        }
        return;
    }
}
