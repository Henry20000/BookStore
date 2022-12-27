using BookStore.Areas.Identity.Data;
using BookStore.Data;
using BookStore.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class AdminController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly BookStoreContext _context;

        public AdminController(UserManager<AppUser> userManager, BookStoreContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var roleAdmin = _context.Roles.Where(r => r.Name == "Customer").First();
            List<string> customerIds = _context.UserRoles.Where(u => u.RoleId == roleAdmin.Id).Select(u => u.UserId).ToList();
            var customers = _userManager.Users.Where(c => customerIds.Contains(c.Id)).ToList();
            ViewData["Customers"] = customers;
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult StoreOwner()
        {
            var roleAdmin = _context.Roles.Where(r => r.Name == "Seller").First();
            List<string> customerIds = _context.UserRoles.Where(u => u.RoleId == roleAdmin.Id).Select(u => u.UserId).ToList();
            var customers = _userManager.Users.Where(c => customerIds.Contains(c.Id)).ToList();
            ViewData["Customers"] = customers;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ResetPassword(ChangePassword changePassword)
        {
            var user = await _userManager.FindByIdAsync(changePassword.Id);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, changePassword.Password);
            return View("Index");
        }
    }
}
