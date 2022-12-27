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

namespace BookStore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly BookStoreContext _context;
        protected readonly UserManager<AppUser> _userManager;

        public OrdersController(BookStoreContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            string thisUserId = _userManager.GetUserId(HttpContext.User);
            var cart = await _context.Order
            .Include(c => c.User)
            .ToListAsync();
            return View(_context.Order.Where(c => c.UId == thisUserId));
        }
        public async Task<IActionResult> Manager()
        {
            var cart = await _context.Order
            .Include(c => c.User)
            .ToListAsync();
            return View(_context.Order);
        }
    }
}
