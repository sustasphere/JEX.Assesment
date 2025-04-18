namespace JEX.Assessment.Domain.V1.Types.Entities;

public class Forecast
{
    static readonly Random rnd = Random.Shared;
    static readonly string [] Summaries
        = [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            ];

    public int TemperatureF => 32 + (int)( TemperatureC / 0.5556 );

    public required DateOnly Date { get; init; }
    public required int TemperatureC { get; init; }
    public required string Summary { get; init; }

    public static Forecast Generate( int day )
    => new() {
        Date = DateOnly.FromDateTime( DateTime.Now.AddDays( day ) ),
        TemperatureC = rnd.Next( -20, 55 ),
        Summary = Summaries [ rnd.Next( Summaries.Length ) ]
    };
}
