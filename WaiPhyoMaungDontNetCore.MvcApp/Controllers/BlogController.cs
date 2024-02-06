using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaiPhyoMaungDontNetCore.MvcApp.Models;
using System.Collections.Generic; // Add this namespace for List<T>

namespace WaiPhyoMaungDontNetCore.MvcApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _appdbcontext;

        public BlogController(AppDbContext context)
        {
            _appdbcontext = context;
        }

        public async Task<IActionResult> Index()
        {
            List<BlogDataModel> blogs = await _appdbcontext.Blogs.ToListAsync();
            return View(blogs); // Pass the list of blogs to the view
        }
    }
}
