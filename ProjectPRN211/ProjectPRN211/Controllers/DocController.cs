using Microsoft.AspNetCore.Mvc;
using ProjectPRN211.Logics;
using ProjectPRN211.Models;
using System;
using System.Collections.Generic;

namespace ProjectPRN211.Controllers
{
    public class DocController : Controller
    {
        public IActionResult CreateDoc()
        {
            HospitalManager hospitalManager = new HospitalManager();
            List<Hospital> list = hospitalManager.GetHospitals();
            return View(list);
        }

        public IActionResult DoCreateDoc(string param1, string param2, string param3, string param4)
        {
            if (param1.Length == 0 || param2.Length == 0 || param3.Length == 0 || param4.Length == 0)
            {
                ViewBag.error = "Please fill all fields!";
                return View("/Views/Doc/CreateDoc.cshtml");
            }
            else
            {
                int hosId = Convert.ToInt32(param3.Trim());
                DateTime date = Convert.ToDateTime(param4.Trim());
                Document doc = new Document();
                doc.HospitalId = hosId;
                doc.DocSubject = param1.Trim();
                doc.DocText = param2.Trim();
                doc.DocDate = date;
                DocManager docManager = new DocManager();
                docManager.CreateDoc(doc);
                return RedirectToAction("ListDoc");
            }
        }

        public IActionResult ListDoc()
        {
            return View();
        }
    }
}
