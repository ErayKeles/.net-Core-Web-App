// Views/ViewComponents/UserMenuViewComponent.cs

using System.Threading.Tasks;
using AspNetCoreIdentityApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentityApp.Views.ViewComponents
{
    public class UserMenuViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public UserMenuViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user != null)
            {
                var viewModel = new UserMenuViewModel
                {
                    UserName = user.UserName,
                    Email = user.Email
                    // Add more properties as needed
                };

                return View(viewModel);
            }

            // If user is not authenticated, you can handle it accordingly
            return Content("User not authenticated");
        }
    }

    public class UserMenuViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        // Add more properties as needed
    }
}
