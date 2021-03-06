﻿using System.Threading.Tasks;
using DialogFlow.Sdk.Builders;
using DialogFlow.Sdk.Models.Fulfillment;
using Jobber.Sdk;
using Jobber.SmartAssistant.Core;

namespace Jobber.SmartAssistant.Features.FavoriteNumber
{
    public class FavoriteNumberIntentFulfiller : IJobberIntentFulfiller
    {
        public bool CanFulfill(FulfillmentRequest fulfillmentRequest)
        {
            return fulfillmentRequest.IsForAction("FAVORITE_NUMBER");
        }

        public async Task<FulfillmentResponse> FulfillAsync(FulfillmentRequest fulfillmentRequest, IJobberClient jobberClient)
        {
            var num = fulfillmentRequest.GetParameterAsInt("num");

            return FulfillmentResponseBuilder.Create()
                .Speech($"Your favorite number is {num}")
                .Build();
        }
    }
}