﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DialogFlow.Sdk.Builders;
using DialogFlow.Sdk.Models.Fulfillment;
using DialogFlow.Sdk.Models.Messages;
using Jobber.Sdk;
using Jobber.Sdk.Models.Jobs;
using Jobber.SmartAssistant.Core;
using Jobber.SmartAssistant.Extensions;
using Jobber.SmartAssistant.GoogleMaps;

namespace Jobber.SmartAssistant.Features.GetNextVisit
{
    public class GetNextVisitIntentFulfiller : IJobberIntentFulfiller
    {
        public bool CanFulfill(FulfillmentRequest fulfillmentRequest)
        {
            return fulfillmentRequest.IsForAction(Constants.Intents.GetNextVisit);
        }

        public async Task<FulfillmentResponse> FulfillAsync(FulfillmentRequest fulfillmentRequest,
            IJobberClient jobberClient)
        {
            var userId = fulfillmentRequest.GetCurrentUserId();
            var current_time = DateTime.Now.ToUnixTime();
            var visits = await jobberClient.GetTodayAssignedVisitsAsync(userId, current_time);
            if (visits.Count == 0)
            {
                return FulfillmentResponseBuilder.Create()
                    .Speech($"Your remaining day looks clear")
                    .Build();    
            }
            return FulfillmentResponseBuilder.Create()
                .Speech(BuildResponseFrom(visits.Visits.First()))
                .WithMessage(BuildGoogleCardFrom(visits.Visits.First()))
                .Build(); 
        }

        private static string BuildResponseFrom(Visit visit)
        {
            string response = _BuildResponseFrom(visit);
            if (visit.MyJob != null)
            {
                if (!visit.MyJob.Notes.Any())
                {
                    return response;
                }    
                StringBuilder sb = new StringBuilder();
                foreach (Note note in visit.MyJob.Notes)
                {
                    sb.Append(note.Message);
                }
                return response + $"Job notes are {sb.ToString()}.";
            }
            return response;
        }

        private static string _BuildResponseFrom(Visit visit)
        {
            float fromNow = visit.StartAt - DateTime.Now.ToUnixTime();
            float durationFromNow = fromNow / 3600;
            int hoursFromNow = (int)Math.Floor(durationFromNow);
            int minutesFromNow = (int)((durationFromNow - hoursFromNow) * 60);

            float length = visit.EndAt - visit.StartAt;
            float duration = length / 3600;
            int hours = (int)Math.Floor(duration);
            int minutes = (int)((duration - hours) * 60);

            StringBuilder sb = new StringBuilder();
            sb.Append($"Your next visit is for {visit.Title} ");
            if (!string.IsNullOrEmpty(visit.Description))
            {
                sb.Append($"Decription is {visit.Description}. ");
            }

            if (hoursFromNow == 0 && minutesFromNow == 0)
            {
                sb.Append("right now. ");
            }
            else
            {
                sb.Append($"in {hoursFromNow} hours and {minutesFromNow} minutes. ");
            }

            if (hours == 0 && minutes == 0 || hours >= 12)
            {
                sb.Append($"Looks like this visit lasts all day. ");
            }
            else
            {
                sb.Append($"This visit is for {hours} hours and {minutes} minutes.");
            }

            return sb.ToString().Trim();
        }
        
        private static GoogleCardMessage BuildGoogleCardFrom(Visit visit)
        {
            if (visit.MyProperty != null)
            {
                var mapImage = GoogleMapsHelper.GetStaticMapLinkFor(visit.MyProperty.MapAddress);
                var mapLink = GoogleMapsHelper.GetGoogleMapsLinkFor(visit.MyProperty.MapAddress);
                return GoogleCardBuilder.Create()
                    .Title($"Visit {visit.Title}")
                    .Content($"At {visit.MyProperty.MapAddress}.")
                    .Image(mapImage, "Map of visit location.")
                    .WithButton("Open Map", mapLink)
                    .Build();    
            }
            return GoogleCardBuilder.Create()
                .Title($"Visit {visit.Title}")
                .Build();     
        }
    }
}