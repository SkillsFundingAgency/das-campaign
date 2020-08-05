using System.Text;

namespace Sfa.Das.Sas.Web.Services.MappingActions.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using ApplicationServices.Models;

    public static class ProviderMappingHelper
    {
        public static string GetPercentageText(double? level)
        {
            return level == null ? "no data available" : $"{level}%";
        }

        public static string GetPercentageText(double? level, bool isHei)
        {
            if (level == null && isHei)
            {
                return "not currently collected for this training organisation";
            }

            return level == null ? "no data available" : $"{level}%";
        }

        public static string GetDeliveryOptionText(List<string> deliveryOptions)
        {
            var deliveryOptionsMessage = new StringBuilder();

            deliveryOptionsMessage.Append("<div class='icon-alerts'><p class='icon-right inline-alert'>");

            deliveryOptionsMessage.Append(ProcessDeliveryModeToRedCrossOrGreenTick(deliveryOptions != null && deliveryOptions.Contains("DayRelease"), "day release"));
            deliveryOptionsMessage.Append(ProcessDeliveryModeToRedCrossOrGreenTick(deliveryOptions != null && deliveryOptions.Contains("BlockRelease"), "block release"));
            deliveryOptionsMessage.Append(ProcessDeliveryModeToRedCrossOrGreenTick(deliveryOptions != null && deliveryOptions.Contains("100PercentEmployer"), "at your location"));

            deliveryOptionsMessage.Append("</p></div>");
            return $"{deliveryOptionsMessage}";
        }

        public static string GetCommaList(params string [] list)
        {
            return string.Join(", ", list.Where(m => !string.IsNullOrWhiteSpace(m)));
        }

        private static string ProcessDeliveryModeToRedCrossOrGreenTick(bool status, string desc)
        {
            return status
                ? $"<span class='icon-content'>{desc}</span><span class='green-tick'></span>"
                : $"<span class='icon-content'>{desc}</span><span class='red-cross'></span>";
        }

        public static string GetLocationAddressLine(ProviderSearchResultItem providerLocation)
        {
            return GetCommaList(
                providerLocation.LocationName,
                providerLocation.Address.Address1,
                providerLocation.Address.Address2,
                providerLocation.Address.Town,
                providerLocation.Address.County,
                providerLocation.Address.Postcode);
        }
    }
}