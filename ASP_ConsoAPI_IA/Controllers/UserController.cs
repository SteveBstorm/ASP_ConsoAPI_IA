using ASP_ConsoAPI_IA.Models;
using ASP_ConsoAPI_IA.Utils;
using Dal.Interface;
using Dal.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ConsoAPI_IA.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;
        private SessionManager _sessionManager;

        public UserController(IUserService userService, SessionManager sessionManager)
        {
            _userService = userService;
            _sessionManager = sessionManager;
        }

        
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(AuthRegisterViewModel form)
        {
            if (!ModelState.IsValid) return View(form);

            _userService.Register(form.ToDal());
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AuthLoginViewModel form)
        {
            if (!ModelState.IsValid) return View(form);

            User connectedUser = _userService.Login(form.Idenfifiant, form.Password);

            _sessionManager.CurrentUser = connectedUser;

            return RedirectToAction("Index","Message");
        }

        public IActionResult Logout()
        {
            _sessionManager.LogOut();
            return RedirectToAction("Index", "Message");
        }
    }
}
