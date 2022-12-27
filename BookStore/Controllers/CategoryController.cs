using BookStore.Areas.Identity.Data;
using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly BookStoreContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CategoryController(BookStoreContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Categories
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> Index()
        {

            var userContext = _context.Category.Include(s => s.User);
            return View(await userContext.ToListAsync());
        }

        // GET: Category/Create
        [Authorize(Roles = "Seller")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> Create([Bind("Name")] Category category)
        {
            AppUser ThisUres = await _userManager.GetUserAsync(HttpContext.User);
            Category categoryExisted = await _context.Category.FirstOrDefaultAsync(s => s.Name == category.Name);
            if (categoryExisted != null)
            {
                ViewData["error"] = "Category have existed!";
                return View();
            }
            category.User = ThisUres;
            _context.Category.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
