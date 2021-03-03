using System;

namespace HRSC.Core.Models
{
    public class User : BaseModel
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
        public bool IsAdmin { get; set; }
    }
}
