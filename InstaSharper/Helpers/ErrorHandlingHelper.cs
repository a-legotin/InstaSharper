using System;
using InstaSharper.Classes.ResponseWrappers;
using Newtonsoft.Json;

namespace InstaSharper.Helpers
{
    internal static class ErrorHandlingHelper
    {
        internal static BadStatusResponse GetBadStatusFromJsonString(string json)
        {
            var badStatus = new BadStatusResponse();
            try
            {
                if (string.IsNullOrEmpty(json))
                {
                    badStatus.ErrorType = "Unknown";
                    badStatus.Message = "No info about error received from IG";
                }
                else if (json.Contains("Oops, an error occurred"))
                {
                    badStatus.ErrorType = "IG server reported error";
                    badStatus.Message = json;
                }
                else badStatus = JsonConvert.DeserializeObject<BadStatusResponse>(json);
            }
            catch (Exception ex)
            {
                badStatus.Message = ex.Message;
            }

            return badStatus;
        }
    }
}