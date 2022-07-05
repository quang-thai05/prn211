using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectPRN211.Logics;
using ProjectPRN211.Models;

namespace ProjectPRN211.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string param1, string param2)
        {
            string userStr = HttpContext.Session.GetString("user");
            if (string.IsNullOrEmpty(userStr))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public IActionResult DoLogin(string param1, string param2)
        {
            UserManager userManager = new UserManager();
            User user = userManager.GetUser(param1, param2);
            if (user != null)
            {
                if (user.Active)
                {
                    HttpContext.Session.SetString("user", JsonConvert.SerializeObject(user));
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Account isn't active, please active!";
                    return View("/Views/Home/Active.cshtml");
                }
            }
            else
            {
                ViewBag.email = param1;
                ViewBag.password = param2;
                ViewBag.Error = "Please check your email or password!";
                return View("/Views/Home/Login.cshtml");
            }
        }

        public IActionResult Active()
        {
            ViewBag.error = "Your account hasn't active yet, please enter OTP in email to active!";
            return View();
        }

        public IActionResult DoActive(string param1, string param2)
        {
            UserManager userManager = new UserManager();
            User user = userManager.GetUserByEmail(param1);
            if (user != null)
            {
                if(user.Otp.Equals(param2))
                {
                    userManager.ActiveAccount(user.UserId);
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.email = param1;
                    ViewBag.otp = param2;
                    ViewBag.wrongotp = "OTP is wrong!";
                    return View("/Views/Home/Active.cshtml");
                }
            }
            else
            {
                ViewBag.wrongemail = "User not found";
                ViewBag.email = param1;
                ViewBag.otp = param2;
                return View("/Views/Home/Active.cshtml");
            }
        }

        public IActionResult DoLogout()
        {
            HttpContext.Session.Remove("user");
            return RedirectToAction("Login");
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult DoRegister(string param1, string param2, string param3, string param4)
        {
            UserManager userManager = new UserManager();
            User u = userManager.GetUserByEmail(param1);
            if (u != null)
            {
                ViewBag.error = "This email has been use, please enter again!";
                return View("/Views/Home/Register.cshtml");
            }
            else
            {
                if (!param2.Equals(param3))
                {
                    ViewBag.error = "Confirm password fail, please enter again!";
                    ViewBag.username = param1;
                    ViewBag.email = param2;
                    ViewBag.pass = param3;
                    ViewBag.repass = param4;
                    return View("/Views/Home/Register.cshtml");
                }
                else
                {
                    User u2 = new User();
                    u2.UserName = param1;
                    u2.Email = param2;
                    u2.Password = param3;
                    userManager.Insert(u2);
                }
            }
        }
    }
}
