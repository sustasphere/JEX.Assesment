﻿using JEX.Assessment.Domain.V1.Types.Exceptions;
using JEX.Assessment.Domain.V1.Types.Messages;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace JEX.Assessment.Domain.V1.Behavior.Filters;

public class GenerateCompanyFilter( ILogger<GenerateCompanyFilter> logger ) : IFilter<ConsumeContext<GenerateCompany>>
{
    public void Probe( ProbeContext ctx )
        => ctx.CreateFilterScope( "validation" );

    public async Task Send( ConsumeContext<GenerateCompany> ctx, IPipe<ConsumeContext<GenerateCompany>> next )
    {
        if ( ctx.Message.Count > 0 )
        {
            await next.Send( ctx );
        }
        else
        {
            var errorMessage = "Count should be larger than 0";
            logger.LogError( $"[{DateTime.Now}] Validation failed on message: {ctx.Message.ToString()}; due to: {errorMessage}" );

            throw new DomainException( errorMessage );
        }
    }
}
