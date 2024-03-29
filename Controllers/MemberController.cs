﻿using AspNetCoreIdentityApp.Extensions;
using AspNetCoreIdentityApp.Models;
using AspNetCoreIdentityApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace AspNetCoreIdentityApp.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFileProvider _fileProvider;

        public MemberController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IFileProvider fileProvider)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _fileProvider = fileProvider;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);

            var userViewModel = new UserViewModel
            {
                UserName = currentUser.UserName,
                Email = currentUser.Email,
                PhoneNumber = currentUser.PhoneNumber,
                PictureURL = currentUser.Picture
            };

            return View(userViewModel);
        }







        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }







        [HttpGet]
        public IActionResult PasswordChange()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PasswordChange(PasswordChangeViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var checkOldPassword = await _userManager.CheckPasswordAsync(currentUser, request.OldPassword);

            if (checkOldPassword == false)
            {
                ModelState.AddModelError(string.Empty, "Eski şifrenizi yanlış girdiniz.");
                return View();
            }

            if (request.OldPassword == request.NewPassword)
            {
                ModelState.AddModelError(string.Empty, "Yeni şifreniz eski şifrenizle aynı olamaz.");
                return View();
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(currentUser, request.OldPassword, request.NewPassword);

            if (!changePasswordResult.Succeeded)
            {
                ModelState.AddModelErrorList(changePasswordResult.Errors);
                return View();
            }

            //Şifre değiştirdikten sonra cookie yenilenmesi için kullanıcıya girdi çıktı yaptırıyoruz.
            await _userManager.UpdateSecurityStampAsync(currentUser);
            await _signInManager.SignOutAsync();
            await _signInManager.PasswordSignInAsync(currentUser, request.NewPassword, true, true);


            TempData["PasswordChangeSucceedMessage"] = "Şifreniz Başarıyla Güncellenmiştir.";


            return View();
        }






        [HttpGet]
        public async Task<IActionResult> UserEdit()
        {
            ViewBag.genderList = new SelectList(Enum.GetNames(typeof(Gender)));

            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var userEditViewModel = new UserEditViewModel()
            {
                UserName = currentUser.UserName,
                Email = currentUser.Email,
                Phone = currentUser.PhoneNumber,
                City = currentUser.City,
                DateOfBirth = currentUser.DateOfBirth,
                Gender = currentUser.Gender
            };

            return View(userEditViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(UserEditViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            //giriş yapan kullanıcıyı bul
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);

            //requestten yani kullanıcının talebinden gelen verileri aşağıdakilerle değiştir.
            currentUser.UserName = request.UserName;
            currentUser.Email = request.Email;
            currentUser.PhoneNumber = request.Phone;
            currentUser.City = request.City;
            currentUser.DateOfBirth = request.DateOfBirth;
            currentUser.Gender = request.Gender;


            if (request.Picture != null && request.Picture.Length > 0)
            {
                var wwwroot = _fileProvider.GetDirectoryContents("wwwroot");
                var randomFileName = $"{Guid.NewGuid()}{Path.GetExtension(request.Picture.FileName)}"; // .jpg .png vs vs. / NewGuid().ToString() kaldırıldı sorun çıkarsa ekle
                var newPicturePath = Path.Combine(wwwroot.First(a => a.Name == "userpictures").PhysicalPath, randomFileName);

                using var stream = new FileStream(newPicturePath, FileMode.Create);

                await request.Picture.CopyToAsync(stream);

                currentUser.Picture = randomFileName;
            }

            var userUpdateResult = await _userManager.UpdateAsync(currentUser);

            if (!userUpdateResult.Succeeded)
            {
                ModelState.AddModelErrorList(userUpdateResult.Errors);
                return View();
            }

            await _userManager.UpdateSecurityStampAsync(currentUser);
            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(currentUser, true);


            TempData["UserEditSucceedMessage"] = "Bilgileriniz Başarıyla Güncellenmiştir.";

            var userEditViewModel = new UserEditViewModel()
            {
                UserName = currentUser.UserName,
                Email = currentUser.Email,
                Phone = currentUser.PhoneNumber,
                City = currentUser.City,
                DateOfBirth = currentUser.DateOfBirth,
                Gender = currentUser.Gender,
            };

            return View(userEditViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AccessDenied(string ReturnURL)
        {
            string message = string.Empty;
            string message2 = string.Empty;

            message = "Bu Sayfayı Görmek İçin Yetkiniz Yok !";
            message2 = "Yetkilendirme için lütfen yöneticiniz ile görüşünüz.";

            ViewBag.message = message;
            ViewBag.message2 = message2;

            return View();
        }
    }
}
