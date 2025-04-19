using JEX.Assessment.Domain.V1.Behavior.Consumers;
using JEX.Assessment.Domain.V1.Behavior.Filters;
using JEX.Assessment.Domain.V1.Types.Messages;
using MassTransit;

namespace JEX.Assessment.API.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddMessageHandling( this IServiceCollection services )
    {
        services.AddMassTransit( cfg => {
            cfg.AddConsumer<GenerateVacancyConsumer>();
            cfg.AddConsumer<GenerateCompanyConsumer>();

            cfg.UsingInMemory( ( ctx, cfg ) => {
                cfg.ReceiveEndpoint( GenerateCompany.QueueName, cfg => {
                    cfg.ConfigureConsumer<GenerateCompanyConsumer>( ctx, cfg => {
                        cfg.UseDelayedRedelivery( cfg => {
                            cfg.Intervals( TimeSpan.FromMinutes( 6 ), TimeSpan.FromMinutes( 12 ), TimeSpan.FromMinutes( 30 ) );
                        } );
                        cfg.UseMessageRetry( cfg => {
                            cfg.Immediate( 6 );
                        } );
                        cfg.UseInMemoryOutbox( ctx, cfg => {
                            cfg.ConcurrentMessageDelivery = true;
                        } );
                    } );
                } );
                cfg.ReceiveEndpoint( GenerateVacancy.QueueName, cfg => {
                    cfg.ConfigureConsumer<GenerateVacancyConsumer>( ctx, cfg => {
                        cfg.UseDelayedRedelivery( cfg => {
                            cfg.Intervals( TimeSpan.FromMinutes( 6 ), TimeSpan.FromMinutes( 12 ), TimeSpan.FromMinutes( 30 ) );
                        } );
                        cfg.UseMessageRetry( cfg => {
                            cfg.Immediate( 6 );
                        } );
                        cfg.UseInMemoryOutbox( ctx, cfg => {
                            cfg.ConcurrentMessageDelivery = true;
                        } );
                    } );
                } );
                cfg.UseConsumeFilter<GenerateCompanyFilter>( ctx );
                cfg.UseConsumeFilter<GenerateVacancyFilter>( ctx );
                cfg.ConfigureEndpoints( ctx );
            } );
        } );

        return services;
    }
}
