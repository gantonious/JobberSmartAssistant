﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Assistant.Sdk.Core;
using DialogFlow.Sdk;
using DialogFlow.Sdk.Intents;

namespace Assistant.Sdk.BuiltIns
{
    public class DialogFlowIntentSynchronizer : IIntentSynchronizer
    {
        private readonly IDialogFlowService _dialogFlowService;

        public DialogFlowIntentSynchronizer(IDialogFlowService dialogFlowService)
        {
            _dialogFlowService = dialogFlowService;
        }

        public async Task SynchronizeIntentsAsync(IEnumerable<Intent> intents)
        {
            await Task.CompletedTask;
        }
    }
}