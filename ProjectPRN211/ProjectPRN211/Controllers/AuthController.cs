using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectPRN211.Logics;
using ProjectPRN211.Models;
using System;

namespace ProjectPRN211.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login(string param1, string param2)
        {
            string userStr = HttpContext.Session.GetString("user");
            if (string.IsNullOrEmpty(userStr))
            {
                return View();
            }
            else
            {
                return Redirect("/");
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
                    return Redirect("/");
                }
                else
                {
                    ViewBag.Error = "Account isn't active, please active!";
                    return View("/Views/Auth/Active.cshtml");
                }
            }
            else
            {
                ViewBag.email = param1;
                ViewBag.password = param2;
                ViewBag.Error = "Please check your email or password!";
                return View("/Views/Auth/Login.cshtml");
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
                if (user.Otp.Equals(param2))
                {
                    userManager.ActiveAccount(user.UserId);
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.email = param1;
                    ViewBag.otp = param2;
                    ViewBag.wrongotp = "OTP is wrong!";
                    return View("/Views/Auth/Active.cshtml");
                }
            }
            else
            {
                ViewBag.wrongemail = "User not found";
                ViewBag.email = param1;
                ViewBag.otp = param2;
                return View("/Views/Auth/Active.cshtml");
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
                return View("/Views/Auth/Register.cshtml");
            }
            else
            {
                if (!param3.Equals(param4))
                {
                    ViewBag.error = "Confirm password fail, please enter again!";
                    ViewBag.username = param1;
                    ViewBag.email = param2;
                    ViewBag.pass = param3;
                    ViewBag.repass = param4;
                    return View("/Views/Auth/Register.cshtml");
                }
                else
                {
                    User u2 = new User();
                    u2.UserName = param1;
                    u2.Email = param2;
                    u2.Password = param3;
                    u2.DateOfBirth = DateTime.Now;
                    u2.Active = false;
                    u2.Otp = userManager.GenerateOTP();
                    u2.RoleId = 3;
                    u2.HospitalId = 1;
                    SendMail sendMail = new SendMail();
                    sendMail.SendingEmail(param2, "Nghe An health service department", "Your OTP is: " + u2.Otp);
                    userManager.Insert(u2);
                }
                ViewBag.email = param2;
                return View("/Views/Auth/Active.cshtml");
            }
        }

        public IActionResult ChangePassword(int param1)
        {
            ViewBag.id = param1;
            return View();
        }

        public IActionResult DoChangePassword(int param1, string param2, string param3, string param4)
        {
            UserManager userManager = new UserManager();
            User user = userManager.GetUserById(param1);

            if (user != null)
            {
                if (param2.Equals(user.Password))
                {
                    if (param3.Equals(param4))
                    {
                        userManager.ChangePassword(param3, user.UserId);
                        HttpContext.Session.SetString("user", JsonConvert.SerializeObject(user));
                        return Redirect("/home/index");
                    }
                    else
                    {
                        ViewBag.error = "Confirm password faild! Please check your information!";
                        ViewBag.id = param1;
                        ViewBag.oldpass = param2;
                        ViewBag.newpass = param3;
                        ViewBag.confirm = param4;
                        return View("/Views/Auth/ChangePassword.cshtml");
                    }
                }
                else
                {
                    ViewBag.error = "Current password is wrong!";
                    ViewBag.id = param1;
                    ViewBag.oldpass = param2;
                    ViewBag.newpass = param3;
                    ViewBag.confirm = param4;
                    return View("/Views/Auth/ChangePassword.cshtml");
                }
            }
            else
            {
                ViewBag.error = "Current password is wrong!";
                return View("/Views/Auth/ChangePassword.cshtml");
            }
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        public IActionResult DoForgotPassword(string param1)
        {
            UserManager userManager = new UserManager();
            User u = userManager.GetUserByEmail(param1);
            SendMail sendMail = new SendMail();
            if (u != null)
            {
                string newPass = userManager.GenerateRandomPass();
                userManager.ChangePassword(newPass, u.UserId);
                sendMail.SendingEmail(param1, "Reset Password", "Your new password is: " + newPass);
                ViewBag.success = "Your password has been reset, check your email to get new password!";
                return View("/Views/Auth/ForgotPassword.cshtml");
            }
            else
            {
                ViewBag.error = "User not found!";
                return View("/Views/Auth/ForgotPassword.cshtml");
            }
        }
    }
}
