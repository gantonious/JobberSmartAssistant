﻿using System;
using System.Threading.Tasks;
using DialogFlow.Sdk.Builders;
using DialogFlow.Sdk.Models.Fulfillment;
using Jobber.Sdk;
using Jobber.SmartAssistant.Core;

namespace Jobber.SmartAssistant.Features.UnassignedVisits
{
    public class UnassignedVisitsFulfiller : IJobberIntentFulfiller
    {

        public bool CanFulfill(FulfillmentRequest fulfillmentRequest)
        {
            return fulfillmentRequest.IsForAction(Constants.Intents.UnassignedVisits);
        }

        public async Task<FulfillmentResponse> FulfillAsync(FulfillmentRequest fulfillmentRequest, IJobberClient jobberClient)
        {
            var today = DateTime.Now;
            var tomorrow = today.AddDays(1);

            var t = new DateTime(today.Year, today.Month, today.Day) - new DateTime(1970, 1, 1);
            var tmr = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day) - new DateTime(1970, 1, 1);

            int secondsSinceEpochToday = (int)t.TotalSeconds;
            int secondsSinceEpochTomorrow = (int)tmr.TotalSeconds;

            var Visits = await jobberClient.GetTodaysVisitsAsync(secondsSinceEpochToday, secondsSinceEpochTomorrow);
            var numOfUnassignedVisits = Visits.NumUnassigned;

            switch (numOfUnassignedVisits)
            {
                case 0:
                    return BuildZeroUnassignedVisitsFoundResponse(numOfUnassignedVisits);
                case 1:
                    return BuildSingleUnassignedVisitsFoundResponse(numOfUnassignedVisits);
                default:
                    return BuildMultipleUnassignedVisitsFoundResponse(numOfUnassignedVisits);
            }
        }

        private static FulfillmentResponse BuildMultipleUnassignedVisitsFoundResponse(int unassignedVisits)
        {
            return FulfillmentResponseBuilder.Create()
                .Speech($"You have {unassignedVisits} unassigned visits for today.")
                .Build();
        }

        private static FulfillmentResponse BuildSingleUnassignedVisitsFoundResponse(int unassignedVisits)
        {
            return FulfillmentResponseBuilder.Create()
                .Speech("You have 1 visit left to be assigned today.")
                .Build();
        }

        private static FulfillmentResponse BuildZeroUnassignedVisitsFoundResponse(int unassginedVisits)
        {
            return FulfillmentResponseBuilder.Create()
                .Speech("There are no visits left to be assigned today!")
                .Build();
        }
    }
}
