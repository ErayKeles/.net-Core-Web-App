// Areas/Admin/Controllers/BeeHiveController.cs
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AspNetCoreIdentityApp.Data;
using AspNetCoreIdentityApp.Models;
using AspNetCoreIdentityApp.Areas.Admin.ViewModels;
using AspNetCoreIdentityApp.Migrations;

namespace AspNetCoreIdentityApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BeehiveController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDBContext _dbContext;

        public BeehiveController(UserManager<AppUser> userManager, AppDBContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var beeHives = _dbContext.BeeHives
                .Include(b => b.User)
                .ToList();

            // AdminBeeHiveViewModel'e dönüştürme
            var adminViewModels = beeHives.Select(b => new AdminBeeHiveViewModel
            {
                Id = b.Id,
                Location = b.Location,
                ProductionDate = b.ProductionDate,
                Capacity = b.Capacity,
                UserId = b.UserId,
                UserName = b.User.UserName,
                UserEmail = b.User.Email
            }).ToList();

            return View(adminViewModels);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var beeHive = _dbContext.BeeHives
                .Include(b => b.User)
                .FirstOrDefault(b => b.Id == id);

            if (beeHive == null)
            {
                return NotFound();
            }

            var model = new AdminBeeHiveViewModel
            {
                Id = beeHive.Id,
                Location = beeHive.Location,
                ProductionDate = beeHive.ProductionDate,
                Capacity = beeHive.Capacity,
                UserId = beeHive.UserId,
                UserName = beeHive.User.UserName,
                UserEmail = beeHive.User.Email
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(AdminBeeHiveViewModel model)
        {
            if (ModelState.IsValid)
            {
                var beeHive = _dbContext.BeeHives.FirstOrDefault(b => b.Id == model.Id);

                if (beeHive == null)
                {
                    return NotFound();
                }

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
            var beeHive = _dbContext.BeeHives
                .Include(b => b.User)
                .FirstOrDefault(b => b.Id == id);

            if (beeHive == null)
            {
                return NotFound();
            }

            var model = new AdminBeeHiveViewModel
            {
                Id = beeHive.Id,
                Location = beeHive.Location,
                ProductionDate = beeHive.ProductionDate,
                Capacity = beeHive.Capacity,
                UserId = beeHive.UserId,
                UserName = beeHive.User.UserName
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
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
