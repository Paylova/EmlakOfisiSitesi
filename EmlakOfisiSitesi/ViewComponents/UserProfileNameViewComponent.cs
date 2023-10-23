using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmlakOfisiSitesi.ViewComponents
{
    public class UserProfileNameViewComponent : ViewComponent
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserProfileNameViewComponent(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = Request.Cookies["userId"];
            if (!string.IsNullOrEmpty(userId))
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    return View(user);
                }
            }
            return View();
        }
    }
}
