using HRSC.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRSC.Web.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display (Name = "Email address")]
        [EmailAddress (ErrorMessage = "Invalid Email Format")]
        public string LoginEmail { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class CreateEmployeeViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "TIN")]
        public string TIN { get; set; }

        [Required]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Employee Type")]
        public Guid EmployeeType { get; set; }
    }

    public class DetailsViewModel
    {
        public Employee Employee { get; set; }
        public IEnumerable<Payment> Payments { get; set; }

        public double DaysAbsence { get; set; }
        public double DaysWorked { get; set; }

        [Required]
        [Display(Name = "Payment Month")]
        public string Month { get; set; }

        public Guid EmployeeId { get; set; }


    }
}
