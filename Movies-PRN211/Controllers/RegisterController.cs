using Microsoft.AspNetCore.Mvc;
using Movies_PRN211.Interfaces;
using Movies_PRN211.Models;

namespace Movies_PRN211.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IAccountRepository _accountRepository;


        public RegisterController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public IActionResult Register()
        {
            if (TempData["errorExist"] != null)
            {
                ViewBag.exit = TempData["errorExist"];
            }
            return View(new Account());
        }

        [HttpPost]
        public IActionResult Register(Account model, string RePassword, string Password)
        {
            if (ModelState.IsValid)
            {
                var checkExit = _accountRepository.isExits(model.Gmail);
                if (checkExit)
                {
                    TempData["errorExist"] = "User Name Is Exist";
                    ViewBag.exit = TempData["errorExist"];
                    return View(model);
                }
                else if(Password.Length<=8 )
                {
                    TempData["errorExist"] = "Password must be more than 8 characters";
                    ViewBag.exit = TempData["errorExist"];
                    return View(model);
                }
                else if (RePassword==""|| RePassword != Password)
                {
                    TempData["errorExist"] = "Password and Repassword do not match";
                    ViewBag.exit = TempData["errorExist"];
                    return View(model);
                }
                else
                {
                    _accountRepository.AddAccount(model);
                    TempData["success"] = "Your account has just been created successfully";
                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {
                return View(model);
            }
        }
    }
}
