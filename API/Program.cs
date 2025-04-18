using JEX.Assessment.Application.V1.Behavior.Extensions;
using JEX.Assessment.Application.V1.Behavior.Filters;

namespace JEX.Assessment.API;

public class Program
{
    public static void Main( string [] args )
    {
        var builder = WebApplication.CreateBuilder( args );

        builder.Services.AddMessageHandling();

        builder.Services.AddControllers( cfg => {
            cfg.Filters.Add<CatchAllExceptionFilter>();
        } );

        builder.Services.AddOpenApi();

        var app = builder.Build();

        if ( app.Environment.IsDevelopment() )
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
