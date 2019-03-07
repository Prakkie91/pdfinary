using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlToPdf.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HtmlToPdf.Controllers
{
    public class BaseController : Controller
    {
        public void AlertSuccess(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Success, message, dismissable);
        }

        public void AlertInformation(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Information, message, dismissable);
        }

        public void AlertWarning(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Warning, message, dismissable);
        }

        public void AlertDanger(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Danger, message, dismissable);
        }

        private void AddAlert(string alertStyle, string message, bool dismissable)
        {
            var alerts = TempData.ContainsKey(Alert.TempDataKey) ? (JsonConvert.DeserializeObject<List<Alert>>((string)TempData[Alert.TempDataKey])) : new List<Alert>();

            alerts.Add(new Alert
            {
                AlertStyle = alertStyle,
                Message = message,
                Dismissable = dismissable
            });

            TempData[Alert.TempDataKey] = JsonConvert.SerializeObject(alerts);
        }
    }
}