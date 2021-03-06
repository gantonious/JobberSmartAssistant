﻿using System;
using System.Threading.Tasks;
using DialogFlow.Sdk.Builders;
using DialogFlow.Sdk.Models.Fulfillment;
using Jobber.Sdk;
using Jobber.SmartAssistant.Core;
using Jobber.SmartAssistant.Extensions;
using System.Linq;
using DialogFlow.Sdk.Models.Common;
using Jobber.Sdk.Rest.Requests;

namespace Jobber.SmartAssistant.Features.GetRevenue
{
    public class GetRevenueIntentFulfiller : IJobberIntentFulfiller
    {
        public bool CanFulfill(FulfillmentRequest fulfillmentRequest)
        {
            return fulfillmentRequest.IsForAction(Constants.Intents.GetRevenue);
        }

        public async Task<FulfillmentResponse> FulfillAsync(FulfillmentRequest fulfillmentRequest, IJobberClient jobberClient)
        {
            var datePeriod = GetDatePeriodForRevenueFrom(fulfillmentRequest);
            var timeUnit = fulfillmentRequest.GetParameter(Constants.Variables.TimeUnitOriginal);

            if (string.IsNullOrEmpty(timeUnit))
            {
                timeUnit = "last week";
            }

            var getTransactionRequest = new GetTransactionRequest
            {
                Start = datePeriod.Start,
                End = datePeriod.End,
                TimeUnit = timeUnit
            };

            var Transactions = await jobberClient.GetRangedTransactionsAsync(getTransactionRequest);
            decimal revenue = Transactions.GetTotal();

            if (timeUnit.EndsWith("?"))
            {
                timeUnit = timeUnit.Remove(timeUnit.Length - 1);
            }

            return FulfillmentResponseBuilder.Create()
                .Speech($"We made ${revenue.ToString("N")} {timeUnit}")
                .Build();
        }

        private static DatePeriod GetDatePeriodForRevenueFrom(FulfillmentRequest fulfillmentRequest)
        {
            if (fulfillmentRequest.IsParameterDatePeriod(Constants.Variables.TimeUnit))
            {
                return fulfillmentRequest.GetParemterAsDatePeriod(Constants.Variables.TimeUnit);
            }

            return new DatePeriod
            {
                End = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek),
                Start = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 6)
            };
        }
    }
}
