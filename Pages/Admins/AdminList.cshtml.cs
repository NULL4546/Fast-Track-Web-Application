using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fast_Track_Web_Application.Pages.Admins
{
    [Authorize(Roles = "SuperAdmin")]
    public class AdminListModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AdminListModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public List<IdentityUser> Admins { get; set; } = new();

        public async Task OnGetAsync()
        {
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    Admins.Add(user);
                }
            }
        }
    }
}
