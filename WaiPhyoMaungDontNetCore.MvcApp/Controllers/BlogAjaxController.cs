using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaiPhyoMaungDontNetCore.MvcApp.Models;

namespace WaiPhyoMaungDontNetCore.MvcApp.Controllers
{
    public class BlogAjaxController : Controller
    {
        
        private readonly AppDbContext _appdbcontext;

        public BlogAjaxController(AppDbContext context)
        {
            _appdbcontext = context;
        }

        public async Task<IActionResult> Index()
        {
            List<BlogDataModel> blogs = await _appdbcontext.Blogs.OrderByDescending(x => x.Blog_Id).ToListAsync();
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
           int result= await _appdbcontext.SaveChangesAsync();
            return Json(data:new { Message = result > 0 ? "Saving Successful" : "Saving Fail" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(BlogDataModel requestModel)
        {
            var item = await _appdbcontext.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == requestModel.Blog_Id);

            if (item == null)
            {
                return RedirectToAction("Index");
            }

            _appdbcontext.Remove(item);
            int result = await _appdbcontext.SaveChangesAsync();

            return Json(data: new { Message = result > 0 ? "Deleting Successful" : "Deleting Fail" });
        }

        public async Task<ActionResult> Edit(int id)
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
                return Json(data: new { Message = "No data found" });
            }

            item.Blog_Title = requestModel.Blog_Title;
            item.Blog_Author = requestModel.Blog_Author;
            item.Blog_Content = requestModel.Blog_Content;
            int result = await _appdbcontext.SaveChangesAsync();
            return Json(data: new { Message = result > 0 ? "Updating Successful" : "Updating Fail" });
           
        }
    }
}
