using Bogus;
using JEX.Assessment.Domain.V1.Types.Entities;
using JEX.Assessment.Domain.V1.Types.Messages;
using MassTransit;

namespace JEX.Assessment.Domain.V1.Behavior.Consumers;

public class GenerateVacancyConsumer : IConsumer<GenerateVacancy>
{
    readonly Faker _faker = new();

    public async Task Consume( ConsumeContext<GenerateVacancy> ctx )
    {
        List<Vacancy> vacancies = [];
        for ( int idx = 0; idx < ctx.Message.Count; idx++ )
        {
            vacancies.Add( await Vacancy.GenerateAsync( _faker, ctx.CancellationToken ) );
        }

        await ctx.RespondAsync( GeneratedVacancies.Create( ctx.Message.CorrelationId, vacancies ) );
    }
}