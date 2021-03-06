﻿using Assistant.Sdk.BuiltIns;
using DialogFlow.Sdk.Builders;
using DialogFlow.Sdk.Models.Intents;

namespace Jobber.SmartAssistant.Features.CreateJob
{
    public class StartCreateJobIntentDefinition : IIntentDefinition
    {
        public Intent DefineIntent()
        {
            return IntentBuilder.For(Constants.Intents.StartCreateJob)
                .TriggerOn("I want to create a job")
                .TriggerOn("Can you help me with setting up a job?")
                .TriggerOn("I want to make a job")
                .TriggerOn("I need to make a job")
                .TriggerOn("Create a job")
                .TriggerOn("New job")
                .TriggerOn("Make job")
                .TriggerOn("Create job")
                .TriggerOn("Can you make a job?")
                .TriggerOn("Can you create a job?")
                .TriggerOn("Help me make a job")
                .TriggerOn("I need help making a job")
                .TriggerOn("Create me a job")
                .TriggerOn("Create a job for me")
                .RespondsWith("Okay who is this job for?")
                .CreatesContext(ContextBuilder.For(Constants.Contexts.CreateJobClientRequested))
                .Build();
        }
    }
}