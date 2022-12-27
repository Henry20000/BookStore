#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Identity;
using BookStore.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;

namespace BookStore.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly BookStoreContext _context;
        private readonly UserManager<AppUser> _userManager;

        public OrderDetailsController(BookStoreContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        public async Task<IActionResult> Index(int id)
        {
            
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);
            var userContext = _context.OrderDetail
                .Where(o => o.Order.UId == user.Id && o.OrderId == id)
                .Include(o => o.Book).Include(o => o.Order)
                .Include(o => o.Order.User)
                .Include(o => o.Book.Store);
            return View(await userContext.ToListAsync());
        }

        public async Task<IActionResult> Manager(int id)
        {
            var userContext = _context.OrderDetail
                 .Include(o => o.Book)
                 .Include(o => o.Order)
                 .Include(o => o.Order.User)
                 .Include(o => o.Book.Store);
            return View(await userContext.ToListAsync());
        }
     
    }
    
}
