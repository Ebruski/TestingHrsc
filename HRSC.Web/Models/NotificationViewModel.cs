

namespace HRSC.Web.Models
{
    public class NotificationViewModel
    {
        public string Type { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public static NotificationViewModel GetNotification(string type, string title, string message)
        {
            return new NotificationViewModel
            {
                Type = type,
                Title = title,
                Message = message
            };
        }

        public static NotificationViewModel GetError(string title, string message)
        {
            return GetNotification("danger", title, message);
        }

        public static NotificationViewModel GetInfo(string title, string message)
        {
            return GetNotification("info", title, message);
        }

        public static NotificationViewModel GetSuccess(string title, string message)
        {
            return GetNotification("success", title, message);
        }
        public static NotificationViewModel GetWarning(string title, string message)
        {
            return GetNotification("warning", title, message);
        }
    }
}
