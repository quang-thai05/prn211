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
            DocManager docManager = new DocManager();
            List<Document> list = docManager.GetDocuments();
            return View(list);
        }

        public IActionResult Update(int param1)
        {
            HospitalManager hosManager = new HospitalManager();
            List<Hospital> hospitalList = hosManager.GetHospitals();
            ViewBag.hospitalList = hospitalList;
            ViewBag.id = param1;
            DocManager docManager = new DocManager();
            Document doc = docManager.GetDocumetById(param1);
            return View(doc);
        }

        public IActionResult DoUpdate(int param1, string param2, string param3, int param4, DateTime param5)
        {
            if (param2.Length == 0 || param3.Length == 0)
            {
                ViewBag.error = "You need to fill all information!";
                return View("/Views/Doc/Update.cshtml");
            }
            else
            {
                Document doc = new Document();
                doc.DocSubject = param2;
                doc.DocText = param3;
                doc.HospitalId = param4;
                doc.DocDate = param5;
                DocManager docManager = new DocManager();
                docManager.UpdateDoc(doc, param1);
                return RedirectToAction("ListDoc");
            }
        }

        public IActionResult DoDelete(int param1)
        {
            DocManager docManager = new DocManager();
            docManager.DeleteDoc(param1);
            return RedirectToAction("ListDoc");
        }
    }
}
