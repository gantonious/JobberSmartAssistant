﻿using Assistant.Sdk.BuiltIns;
using DialogFlow.Sdk.Builders;
using DialogFlow.Sdk.Models.Common;
using DialogFlow.Sdk.Models.Intents;

namespace Jobber.SmartAssistant.Features.ModifyQuote.RequestingQuoteNumber
{
    public class QuoteNumberRequestedIntentDefinition : IIntentDefinition
    {
        public Intent DefineIntent()
        {
            return IntentBuilder.For(Constants.Intents.QuoteNumberRequestedModifyQuote)
                .RequiresContext(Constants.Contexts.QuoteNumberRequested)
                .TriggerOn($"[{Entity.Number}:{Constants.Variables.QuoteNumber}:2]")
                .RequireParameter(ParameterBuilder.Of(Constants.Variables.QuoteNumber, Entity.Number)
                    .WithPrompt("What was the quote number?")
                )
                .FulfillWithWebhook()
                .Build();
        }
    }
}