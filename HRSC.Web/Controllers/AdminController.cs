using HRSC.Core.DTO;
using HRSC.Core.Managers.Interfaces;
using HRSC.Web.Components;
using HRSC.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HRSC.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IEmployeeManager _employeeManager;
        private readonly IPaymentManager _paymentManager;

        public AdminController(ILogger<AdminController> logger, IEmployeeManager employeeManager, IPaymentManager paymentManager)
        {
            _logger = logger;
            _employeeManager = employeeManager;
            _paymentManager = paymentManager;
        }

        public IActionResult Index()
        {
            if (StaticCache.User == null)
            {                
                AddNotification(NotificationViewModel.GetWarning("Page Not Accessable", $"Login to access page"));
                return RedirectToAction("Index", "Home");
            }
                

            return View();
        }

        [HttpPost]
        public IActionResult CreateEmployee(CreateEmployeeViewModel model)
        {
            if (StaticCache.User == null)
            {
                AddNotification(NotificationViewModel.GetWarning("Page Not Accessable", $"Login to access page"));
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                var request = new EmployeeDTO
                {
                    Name = $"{model.FirstName} {model.LastName}",
                    TIN = model.TIN,
                    EmployeeTypeId = model.EmployeeType,
                    BirthDate = model.BirthDate
                };

                var res = _employeeManager.Create(request);
                if (res.IsSuccess)
                {
                    StaticCache.Employees = _employeeManager.GetAll().Take(20);

                    AddNotification(NotificationViewModel.GetSuccess("New Employee Created", $"A new employee with name {request.Name} was Created successfully"));
                    return RedirectToAction(nameof(Index));
                }                    
                else
                {
                    ModelState.AddModelError("", $"{res.Message ?? "Unable to login at the moment"}");
                    return View(nameof(Index), model);
                }
            }

            return View(nameof(Index), model);
        }

        [HttpPost]
        public IActionResult CalculateSalary(DetailsViewModel model)
        {
            if (StaticCache.User == null)
            {
                AddNotification(NotificationViewModel.GetWarning("Page Not Accessable", $"Login to access page"));
                return RedirectToAction("Index", "Home");
            }



            if (ModelState.IsValid)
            {
                var request = new ComputeSalaryRequest
                {
                    Month = model.Month,
                    EmployeeId = model.EmployeeId,
                    DaysAbsent = model.DaysAbsence,
                    DaysWorked = model.DaysWorked
                };

                var res = _employeeManager.CalculateSalary(request);
                if (res.IsSuccess)
                {
                    StaticCache.Employees = _employeeManager.GetAll().Take(20);

                    TempData["MsgSuccess"] = $"Employee Salary for the month of {model.Month} is {res.Data.Salary}";
                    TempData["MsgError"] = null;

                    return RedirectToAction(nameof(Details), new { employeeId = model.EmployeeId } );
                }
                else
                {
                    TempData["MsgError"] = $"{res.Message ?? "Unable to Compute Salary at the moment"}";
                    TempData["MsgSuccess"] = null;

                    return RedirectToAction(nameof(Details), new { employeeId = model.EmployeeId });
                }
            }

            TempData["MsgError"] = $"Unable to Compute Salary at the moment";
            TempData["MsgSuccess"] = null;

            return RedirectToAction(nameof(Details), new { employeeId = model.EmployeeId });
        }

        public IActionResult Details(Guid employeeId)
        {
            if (StaticCache.User == null)
            {
                AddNotification(NotificationViewModel.GetWarning("Page Not Accessable", $"Login to access page"));
                return RedirectToAction("Index", "Home");
            }

            var emp = _employeeManager.Get(employeeId);

            var paymentHistory = _paymentManager.GetAll(employeeId);

            var model = new DetailsViewModel
            {
                Employee = emp.Data,
                Payments = paymentHistory,
                EmployeeId = emp.Data.Id
            };           

            return View(model);
        }

        protected void AddNotification(NotificationViewModel notification)
        {
            var notif = TempData.Get<List<NotificationViewModel>>("Notifications");

            if (notif == null)
            {
                notif = new List<NotificationViewModel>();
            }

            notif.Add(notification);

            TempData.Put("Notifications", notif);
        }
    }
}
