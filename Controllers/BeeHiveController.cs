using AspNetCoreIdentityApp.Models;
using AspNetCoreIdentityApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AspNetCoreIdentityApp.Extensions;
using AspNetCoreIdentityApp.Services;
using AspNetCoreIdentityApp.Data;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreIdentityApp.Controllers
{
    public class BeeHiveController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDBContext _dbContext;

        public BeeHiveController(UserManager<AppUser> userManager, AppDBContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [Authorize]
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var userBeeHives = _dbContext.BeeHives.Where(b => b.UserId == userId).ToList();
            return View(userBeeHives);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(BeeHiveViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                var newBeeHive = new BeeHive
                {
                    Location = model.Location,
                    ProductionDate = model.ProductionDate,
                    Capacity = model.Capacity,
                    UserId = userId
                };

                _dbContext.BeeHives.Add(newBeeHive);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var userId = _userManager.GetUserId(User);
            var beeHive = _dbContext.BeeHives.FirstOrDefault(b => b.Id == id && b.UserId == userId);

            if (beeHive == null)
            {
                // Kullanıcıya ait olmayan veya bulunmayan bir arı kovanı için hata sayfasına yönlendirme
                return NotFound();
            }

            var model = new BeeHiveViewModel
            {
                Id = beeHive.Id,
                Location = beeHive.Location,
                ProductionDate = beeHive.ProductionDate,
                Capacity = beeHive.Capacity
            };

            return View(model);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Edit(BeeHiveViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                var beeHive = _dbContext.BeeHives.FirstOrDefault(b => b.Id == model.Id && b.UserId == userId);

                if (beeHive == null)
                {
                    // Kullanıcıya ait olmayan veya bulunmayan bir arı kovanı için hata sayfasına yönlendirme
                    return NotFound();
                }

                // Arı kovanını güncelle
                beeHive.Location = model.Location;
                beeHive.ProductionDate = model.ProductionDate;
                beeHive.Capacity = model.Capacity;

                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var userId = _userManager.GetUserId(User);
            var beeHive = _dbContext.BeeHives.FirstOrDefault(b => b.Id == id && b.UserId == userId);

            if (beeHive == null)
            {
                return NotFound();
            }

            var model = new BeeHiveViewModel
            {
                Id = beeHive.Id,
                Location = beeHive.Location,
                ProductionDate = beeHive.ProductionDate,
                Capacity = beeHive.Capacity
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var userId = _userManager.GetUserId(User);
            var beeHive = _dbContext.BeeHives.FirstOrDefault(b => b.Id == id);

            if (beeHive == null)
            {
                return NotFound();
            }

            _dbContext.BeeHives.Remove(beeHive);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }

}
