﻿using Assistant.Sdk.BuiltIns;
using DialogFlow.Sdk.Builders;
using DialogFlow.Sdk.Models.Intents;

namespace Jobber.SmartAssistant.Features.UnassignedVisits
{
    public class UnassignedVisitsIntentDefinition : IIntentDefinition
    {
        public Intent DefineIntent()
        {
            return IntentBuilder.For(Constants.Intents.UnassignedVisits)
                .TriggerOn("How many visits do I still need to assign today?")
                .TriggerOn("How many visits are unassigned today?")
                .TriggerOn("Unassigned visits for today")
                .TriggerOn("Unassigned visits")
                .FulfillWithWebhook()
                .Build();
        }
    }
}
