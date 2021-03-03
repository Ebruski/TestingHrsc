using HRSC.Core.Extensions;
using HRSC.Core.Managers.Interfaces;
using HRSC.Web.Components;
using HRSC.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace HRSC.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IUserManager _userManager;
        private readonly IEmployeeTypeManager _employeeTypeManager;
        private readonly IEmployeeManager _employeeManager;

        private AppSettings _appSettings;
        public AccountController(ILogger<AccountController> logger, IUserManager userManager,
            IEmployeeTypeManager employeeTypeManager, IEmployeeManager employeeManager,
            AppSettings appSettings)
        {
            _logger = logger;
            _userManager = userManager;
            _employeeTypeManager = employeeTypeManager;
            _employeeManager = employeeManager;
            _appSettings = appSettings;
        }

        
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public IActionResult Login([FromForm] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var res = _userManager.Login(model.LoginEmail, model.Password);
                if (res.IsSuccess)
                {
                    StaticCache.User = res.Data;
                    StaticCache.Employees = _employeeManager.GetAll().Take(20);
                    StaticCache.EmployeeTypes = _employeeTypeManager.GetAll().Take(20);
                    StaticCache.Tax = _appSettings.TaxPercentage;
                }                    
                else
                {
                    ModelState.AddModelError("",$"{res.Message ?? "Unable to login at the moment"}");
                    return View(model);
                }

                if (res.Data.IsAdmin)
                    return RedirectToAction("Index", "Admin");

                return Json(res);
            }

            return View(model);
        }
                
        public IActionResult Logout()
        {
            // clear cache on logout
            StaticCache.User = null;
            StaticCache.Employees = null;
            StaticCache.EmployeeTypes = null;



            return RedirectToAction("Index", "Home");
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
