using Bogus;
using JEX.Assessment.Domain.V1.Types.Entities;
using JEX.Assessment.Domain.V1.Types.Messages;
using MassTransit;

namespace JEX.Assessment.Domain.V1.Behavior.Consumers;

public class GenerateCompanyConsumer : IConsumer<GenerateCompany>
{
    readonly Faker _faker = new();

    public async Task Consume( ConsumeContext<GenerateCompany> ctx )
    {
        List<Company> companies = [];
        for ( int idx = 0; idx < ctx.Message.Count; idx++ )
        {
            companies.Add( await Company.GenerateAsync( _faker, ctx.CancellationToken ) );
        }

        await ctx.RespondAsync( GeneratedCompanies.Create( ctx.Message.CorrelationId, companies ) );
    }
}
