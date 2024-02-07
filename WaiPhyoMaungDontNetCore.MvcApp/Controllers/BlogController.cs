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
            List<BlogDataModel> blogs = await _appdbcontext.Blogs.OrderByDescending(x=>x.Blog_Id).ToListAsync();
            return View(blogs); // Pass the list of blogs to the view
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Save(BlogDataModel requestModel)
        {
            await _appdbcontext.Blogs.AddAsync(requestModel);
            await _appdbcontext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _appdbcontext.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);

            if (item == null)
            {
                return RedirectToAction("Index");
            }

            _appdbcontext.Remove(item);
            await _appdbcontext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task <ActionResult> Edit(int id)
        {
            var item = await _appdbcontext.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);

            if (item == null)
            {
                return RedirectToAction("Index");
            }
            return View(item);
        }


      

        [HttpPost]
        public async Task<IActionResult> Update(int id, BlogDataModel requestModel)
        {
            var item = await _appdbcontext.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);

            if (item == null)
            {
                return RedirectToAction("Index");
            }

            item.Blog_Title = requestModel.Blog_Title;
            item.Blog_Author = requestModel.Blog_Author;
            item.Blog_Content = requestModel.Blog_Content;

            await _appdbcontext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // ... (existing code)

    }
}
