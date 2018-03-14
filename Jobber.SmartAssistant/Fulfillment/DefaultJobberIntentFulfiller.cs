﻿using System.Threading.Tasks;
using DialogFlow.Sdk.Fulfillment;
using Jobber.Sdk;

namespace Jobber.SmartAssistant.Fulfillment
{
    public class DefaultJobberIntentFulfiller : IJobberIntentFulfiller
    {
        public bool CanFulfill(FulfillmentRequest fulfillmentRequest)
        {
            return true;
        }

        public async Task<FulfillmentResponse> FulfillAsync(FulfillmentRequest fulfillmentRequest, IJobberService jobberService)
        {
            var response = new FulfillmentResponse
            {
                Speech = "Sorry, I'm not sure what you're asking for.",
                DisplayText = "Sorry, I'm not sure what you're asking for."
            };

            return await Task.FromResult(response);
        }
    }
}