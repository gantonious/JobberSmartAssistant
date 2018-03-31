﻿using Newtonsoft.Json;

namespace Jobber.SmartAssistant.Features
{
    public static class Constants
    {
        public const string AssistantName = "Jobber Bot";

        public static class Intents
        {
            public const string Welcome = "WELCOME_INTENT";
            public const string Fallback = "FALLBACK_INTENT";
            public const string StartCreateJob = "START_CREATE_JOB";
            public const string ClientRequestedCreateJob = "CLIENT_REQUESTED_CREATE_JOB"; 
            public const string DescritptionRequestedCreateJob = "DESCRIPTION_REQUESTED_CREATE_JOB";

            public const string StartModifyQuote = "START_MODIFY_QUOTE";
            public const string DetailsRequestedModifyQuote = "DETAILS_REQUESTED_MODIFY_QUOTE";
            
            public const string GetCompletableVisits = "GET_COMPLETEABLE_VISITS";
            public const string GetCompletableVisitsDetails = "GET_COMPLETEABLE_VISITS_DETAILS";
            public const string DontGetCompletableVisitsDetails = "DONT_GET_COMPLETEABLE_VISITS_DETAILS";
            public const string UnassignedVisits = "UNASSIGNED_VISITS";
            public const string ConvertibleQuotes = "CONVERTIBLE_QUOTES";
            public const string SendableInvoices = "SENDABLE_INVOICES";
            public const string GetRevenue = "GET_REVENUE";
            public const string GetAssignedVisits = "GET_ASSIGNED_VISITS";
            public const string GetAmountVisits = "GET_AMOUNT_VISITS";
            public const string GetLengthWorkday = "GET_LENGTH_WORKDAY";
            public const string GetNextVisit = "GET_NEXT_VISIT";
            public const string Help = "HELP";
        }

        public static class Variables
        {
            public const string ClientName = "CLIENT_NAME";
            public const string CreateJobContext = "CREATE_JOB_CONTEXT";
            public const string ModifyQuoteContext = "MODIFY_QUOTE_CONTEXT";
            public const string JobDescription = "DESCRIPTION";
            public const string Date = "DATE";
            public const string ServiceNames = "SERVICES";
            public const string TimeUnit = "TIME_UNIT";
            public const string TimeUnitOriginal = "TIME_UNIT_ORIGINAL";
        }

        public static class Contexts
        {
            public const string QuoteDetailsRequested = "QUOTE_DETAILS_REQUESTED";
            public const string QuoteDetailsSet = "QUOTE_DETAILS_Set";

            public const string CreateJobClientRequested = "CREATE_JOB_CLIENT_REQUESTED";
            public const string CreateJobClientSet = "CREATE_JOB_CLIENT_SET";
            public const string AskedIfUserWantsCompletableDetails = "ASKED_COMPLETABLE_DETAILS";
        }

    }
}