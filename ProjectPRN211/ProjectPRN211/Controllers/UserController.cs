using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectPRN211.Models;

namespace ProjectPRN211.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Profile()
        {
            User u = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("user"));
            return View(u);
        }
    }
}
