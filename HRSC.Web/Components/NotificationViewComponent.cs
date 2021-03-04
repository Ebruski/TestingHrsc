using HRSC.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HRSC.Web.Components
{
    public class NotificationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var notifications = TempData.Get<List<NotificationViewModel>>("Notifications");

            if (notifications == null)
            {
                notifications = new List<NotificationViewModel>();
            }

            return View("Notification", notifications);
        }
    }
}
