using JEX.Assessment.API.Context;
using JEX.Assessment.API.Extensions;
using JEX.Assessment.Application.V1.Behavior.Filters;
using Microsoft.EntityFrameworkCore;

namespace JEX.Assessment.API;

public class Program
{
    public static void Main( string [] args )
    {
        var builder = WebApplication.CreateBuilder( args );

        builder.Services.AddDbContext<ApiContext>( cfg => {
            cfg.UseSqlServer( builder.Configuration.GetConnectionString( "JexDb" )! );
        } );

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
