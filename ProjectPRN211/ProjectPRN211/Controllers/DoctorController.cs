using Microsoft.AspNetCore.Mvc;
using ProjectPRN211.Logics;
using System.Collections.Generic;
using ProjectPRN211.Models;

namespace ProjectPRN211.Controllers
{
    public class DoctorController : Controller
    {
        public IActionResult DoctorControl()
        {
            UserManager userManager = new UserManager();
            List<User> listDoctor = userManager.GetUsersByRole(2);
            List<User> listPatient = userManager.GetUsersByRole(3);
            ViewBag.doctors = listDoctor;
            ViewBag.patients = listPatient;
            return View();
        }
    }
}
