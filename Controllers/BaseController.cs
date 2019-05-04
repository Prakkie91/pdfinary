using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Pdfinary.Helpers;

namespace Pdfinary.Controllers
{
    public class BaseController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public readonly int _subscriptionId;

        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                string subscriptionIdString = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(a => a.Type == Configs.SubscriptionIdClaim).Value;
                int.TryParse(subscriptionIdString, out _subscriptionId);
            }
        }

        public void AlertSuccess(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Success, message, dismissable, "Success");
        }

        public void AlertInformation(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Information, message, dismissable, "Info");
        }

        public void AlertWarning(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Warning, message, dismissable, "Warning");
        }

        public void AlertDanger(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Danger, message, dismissable, "Error");
        }

        private void AddAlert(string alertStyle, string message, bool dismissable, string title)
        {
            List<Alert> alerts = TempData.ContainsKey(Alert.TempDataKey) ? (JsonConvert.DeserializeObject<List<Alert>>((string)TempData[Alert.TempDataKey])) : new List<Alert>();

            alerts.Add(new Alert
            {
                AlertStyle = alertStyle,
                Message = message,
                Dismissable = dismissable,
                Title = title
            });

            TempData[Alert.TempDataKey] = JsonConvert.SerializeObject(alerts);
        }
    }
}
