using AspNetCoreIdentityApp.Areas.Admin.ViewModels;
using AspNetCoreIdentityApp.Data;
using AspNetCoreIdentityApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AspNetCoreIdentityApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("/Admin/Home")]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDBContext _dbContext;
        public HomeController(UserManager<AppUser> userManager, AppDBContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [Authorize(Roles = "Admin")]
        [Route("/Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [Route("/Admin/UserList")]
        public async Task<IActionResult> UserList(int page = 1, int pageSize = 10)
        {
            // Calculate the number of items to skip
            int skip = (page - 1) * pageSize;

            // Retrieve a paginated list of users from the database
            var userList = await _userManager.Users.Skip(skip).Take(pageSize).ToListAsync();

            // Map the paginated user list to view models
            var userViewModelList = userList.Select(a => new AdminUserViewModel()
            {
                UserID = a.Id,
                UserName = a.UserName,
                UserEmail = a.Email,
                PictureURL = a.Picture
            }).ToList();

            // Pass additional data like page number, page size, and total count to the view
            ViewBag.PageNumber = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = await _userManager.Users.CountAsync();

            return View(userViewModelList);
        }


        [Authorize(Roles = "Admin")]
        [Route("/Admin/AddBeehive/{userId}")]
        public IActionResult AddBeehive(string userId)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            var model = new AdminBeeHiveViewModel2
            {
                UserId = user.Id,
                UserName = user.UserName,
                Location = "", // Add the location property
                ProductionDate = DateTime.Now, // Add the production date property
                Capacity = 0 // Add the capacity property
            };

            return View("AddBeehive", model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/AddBeehive/{userId}")]
        public IActionResult AddBeehive(AdminBeeHiveViewModel2 model)
        {
            if (ModelState.IsValid)
            {
                // Process the form data and save to the database
                var beehive = new BeeHive
                {
                    UserId = model.UserId,
                    Location = model.Location,
                    ProductionDate = model.ProductionDate,
                    Capacity = model.Capacity
                };

                _dbContext.BeeHives.Add(beehive);
                _dbContext.SaveChanges();

                // Set success message
                ViewData["SuccessMessage"] = "Kovan başarıyla eklendi.";

                
            }

            // If the model state is not valid, return the view with validation errors
            return View("AddBeehive", model);
        }


    }
}
