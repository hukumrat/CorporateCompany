using CorporateCompany.Areas.Admin.Data;
using CorporateCompany.Areas.Admin.Models;
using CorporateCompany.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CorporateCompany.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAppRepository _appRepository;

    public AccountController(SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        IAppRepository appRepository)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _appRepository = appRepository;
    }

    [HttpGet]
    [Route("Account/Index")]
    public IActionResult Index()
    {
        return RedirectToAction("Login", "Account");
    }

    [HttpGet]
    [Route("Account/AddUser")]
    public IActionResult AddUser()
    {
        return View();
    }

    [HttpPost]
    [Route("Account/AddUser")]
    public async Task<IActionResult> AddUser(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                _appRepository.IziToast("Kullanıcı başarıyla oluşturuldu.", "Başarılı");
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }

            foreach (var error in result.Errors)
            {
                _appRepository.IziToast("Kullanıcı oluşturulamadı.", "Hata", "error");
                ModelState.AddModelError(String.Empty, error.Description);
            }
        }

        _appRepository.IziToast("Lütfen gerekli alanları doldurunuz.", "Uyarı", "warning");
        return View(model);
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(SignInViewModel model, string? returnUrl)
    {
        if (ModelState.IsValid)
        {
            var result =
                await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                _appRepository.IziToast($"Hoşgeldiniz {model.UserName}", "Başarılı");
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
            }

            ModelState.AddModelError(String.Empty, "Kullanıcı adı veya şifre yanlış.");
            _appRepository.IziToast("Kullanıcı adı veya şifre yanlış.", "Hata", "error");
        }

        return View(model);
    }

    [Route("Account/SignOut")]
    public async Task<IActionResult> SignOut()
    {
        await _signInManager.SignOutAsync();
        return Redirect("/#Home");
    }

    [HttpGet]
    [Route("Account/ListUsers")]
    public IActionResult ListUsers()
    {
        var users = _appRepository.GetUsers();
        return View(users);
    }

    [HttpGet]
    [Route("Account/EditUser")]
    public IActionResult EditUser(string id)
    {
        var user = _appRepository.GetUserById(id);
        UserEditViewModel model = new UserEditViewModel
        {
            Id = id,
            Email = user.Email,
            UserName = user.UserName
        };
        return View(model);
    }

    [HttpPost]
    [Route("Account/EditUser")]
    public async Task<IActionResult> EditUser(UserEditViewModel model)
    {
        if (ModelState.IsValid)
        {
            var userToUpdate = await _userManager.FindByIdAsync(model.Id);
            var currentUser = await _userManager.GetUserAsync(User);
            if (userToUpdate == null)
            {
                return View("404");
            }

            if (userToUpdate == currentUser)
            {
                if (await _userManager.CheckPasswordAsync(userToUpdate, model.Password))
                {
                    userToUpdate.UserName = model.UserName;
                    userToUpdate.Email = model.Email;
                    var result = await _userManager.UpdateAsync(userToUpdate);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(userToUpdate, false);
                        _appRepository.IziToast("Bilgileriniz başarıyla güncellendi.", "Başarılı");
                        return RedirectToAction("ListUsers", "Account");
                    }

                    foreach (var error in result.Errors)
                    {
                        _appRepository.IziToast("Bilgileriniz güncellenemedi.", "Hata", "error");
                        ModelState.AddModelError(String.Empty, error.Description);
                    }
                }
                else
                {
                    _appRepository.IziToast("Lütfen şifrenizi doğru giriniz.", "Hata", "error");
                    return View(model);
                }
            }
            else
            {
                if (await _userManager.CheckPasswordAsync(userToUpdate, model.Password))
                {
                    userToUpdate.UserName = model.UserName;
                    userToUpdate.Email = model.Email;
                    var result = await _userManager.UpdateAsync(userToUpdate);
                    if (result.Succeeded)
                    {
                        _appRepository.IziToast("Bilgileriniz başarıyla güncellendi.", "Başarılı");
                        return RedirectToAction("ListUsers", "Account");
                    }

                    foreach (var error in result.Errors)
                    {
                        _appRepository.IziToast("Bilgileriniz güncellenemedi.", "Hata", "error");
                        ModelState.AddModelError(String.Empty, error.Description);
                    }
                }
                else
                {
                    _appRepository.IziToast("Lütfen şifrenizi doğru giriniz.", "Hata", "error");
                    return View(model);
                }
            }
        }

        _appRepository.IziToast("Lütfen gerekli alanları doldurunuz.", "Uyarı", "warning");
        return View(model);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var userToDelete = await _userManager.FindByIdAsync(id);
        var currentUserId = _userManager.GetUserId(User);
        if (userToDelete == null)
        {
            return View("404");
        }

        if (userToDelete.Id == currentUserId)
        {
            _appRepository.IziToast("Aktif hesap silinemez.", "Hata", "error");
            return RedirectToAction("ListUsers", "Account");
        }

        var result = await _userManager.DeleteAsync(userToDelete);
        if (result.Succeeded)
        {
            _appRepository.IziToast("Hesap başarıyla silindi.", "Başarılı");
            return RedirectToAction("ListUsers", "Account");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return RedirectToAction("ListUsers", "Account");
    }


    [HttpGet]
    [Route("Account/ChangePassword")]
    public IActionResult ChangePassword()
    {
        return View();
    }

    [HttpPost]
    [Route("Account/ChangePassword")]
    public async Task<IActionResult> ChangePassword(PasswordChangeViewModel model)
    {
        if (ModelState.IsValid)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                _appRepository.IziToast("Şifreniz başarıyla değiştirildi.", "Başarılı");
                return View(model);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            _appRepository.IziToast("Mevcut şifre yanlış.", "Hata", "error");
            return View(model);
        }

        _appRepository.IziToast("Lütfen gerekli alanları doldurunuz.", "Uyarı", "warning");
        return View(model);
    }

    [HttpGet]
    [Route("Account/MyAccount")]
    public async Task<IActionResult> MyAccount()
    {
        var userId = _userManager.GetUserId(User);
        var user = await _userManager.FindByIdAsync(userId);
        MyAccountViewModel model = new MyAccountViewModel
        {
            Email = user.Email,
            UserName = user.UserName
        };
        return View(model);
    }

    [HttpPost]
    [Route("Account/MyAccount")]
    public async Task<IActionResult> MyAccount(MyAccountViewModel model)
    {
        var userId = _userManager.GetUserId(User);
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return View("404");
        }

        if (ModelState.IsValid)
        {
            if (await _userManager.CheckPasswordAsync(user, model.CurrentPassword))
            {
                user.Email = model.Email;
                user.UserName = model.UserName;
                var resultUser = await _userManager.UpdateAsync(user);
                if (!resultUser.Succeeded)
                {
                    foreach (var error in resultUser.Errors)
                    {
                        ModelState.AddModelError(String.Empty, error.Description);
                        _appRepository.IziToast("Bilgileriniz güncellenemedi.", "Hata", "error");
                    }
                }

                if (!string.IsNullOrEmpty(model.NewPassword) && !string.IsNullOrEmpty(model.ConfirmPassword))
                {
                    var resultPassword =
                        await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                    if (!resultPassword.Succeeded)
                    {
                        foreach (var error in resultPassword.Errors)
                        {
                            ModelState.AddModelError(String.Empty, error.Description);
                            _appRepository.IziToast("Bilgileriniz güncellenemedi.", "Hata", "error");
                        }
                    }
                }

                await _signInManager.SignInAsync(user, false);
                _appRepository.IziToast("Bilgileriniz başarıyla güncellendi.", "Başarılı");
                return RedirectToAction("MyAccount", "Account");
            }
            else
            {
                _appRepository.IziToast("Mevcut şifre yanlış.", "Hata", "error");
                return View(model);
            }
        }

        _appRepository.IziToast("Lüften gerekli alanları doldurunuz.", "Uyarı", "warning");
        return View(model);
    }
}