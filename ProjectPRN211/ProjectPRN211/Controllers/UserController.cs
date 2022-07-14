using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectPRN211.Logics;
using ProjectPRN211.Models;
using System;

namespace ProjectPRN211.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Profile()
        {
            User u = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("user"));
            return View(u);
        }

        public IActionResult UpdateProfile(int param1, string param2, string param3, string param4, DateTime param5)
        {
            User user = new User();
            user.UserId = param1;
            user.UserName = param2;
            user.Address = param3;
            user.Phone = param4;
            user.DateOfBirth = param5;
            UserManager userManager = new UserManager();
            userManager.Update(user, param1);
            HttpContext.Session.SetString("user", JsonConvert.SerializeObject(user));
            return RedirectToAction("Profile");
        }
    }
}
