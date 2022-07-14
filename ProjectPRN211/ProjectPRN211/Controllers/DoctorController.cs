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

        public IActionResult UpToDoctor(int param1)
        {
            UserManager userManager = new UserManager();
            userManager.UpdateToDoctor(param1);
            return RedirectToAction("DoctorControl");
        }

        public IActionResult UpToPatient(int param1)
        {
            UserManager userManager = new UserManager();
            userManager.UpdateToPatient(param1);
            return RedirectToAction("DoctorControl");
        }

        public IActionResult ChangeHos(int param1)
        {
            HospitalManager hosManager = new HospitalManager();
            List<Hospital> listHospital = hosManager.GetHospitals();
            ViewBag.hospitals = listHospital;
            UserManager userManager = new UserManager();
            User u = userManager.GetUserById(param1);
            return View(u);
        }

        public IActionResult DoChangeHos(int param1, int param2)
        {
            UserManager userManager = new UserManager();
            userManager.ChangeHos(param1, param2);
            return RedirectToAction("DoctorControl");
        }
    }
}
