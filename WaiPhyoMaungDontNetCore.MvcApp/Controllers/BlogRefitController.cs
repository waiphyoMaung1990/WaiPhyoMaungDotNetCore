using Microsoft.AspNetCore.Mvc;
using WaiPhyoMaungDontNetCore.MvcApp.Models;

namespace WaiPhyoMaungDontNetCore.MvcApp.Controllers
{
    public class BlogRefitController : Controller
    {
        //https://github.com/reactiveui/refit
        private readonly IBlogApi _iblogApi;

        public IActionResult RedirecttoAction { get; private set; }

        public BlogRefitController(IBlogApi iblogApi)
        {
            _iblogApi = iblogApi;
        }

        public async Task<IActionResult> Index()
        {
            var lst = await _iblogApi.GetBlogs();
            return View(lst);
        }

        #region Edit
        public async Task<IActionResult> Edit(int id)
        {
            var blog = await _iblogApi.GetBlog(id);
            if (blog is null)
            {
                return NotFound();
            }
            return View(blog);
        }
        #endregion


        #region Update
        public async Task<IActionResult> Update(int id, BlogDataModel model)
        {
            var message = await _iblogApi.UpdateBlog(id, model);
            if (message is null)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            var message = await _iblogApi.DeleteBlog(id);
            if (message is null)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Create
        public async Task<IActionResult>Save(BlogDataModel model)
        {
            var message = await _iblogApi.CreateBlog(model);
            if (message is null)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region
        public IActionResult Create()
        {
            return View();
        }
        #endregion
    }
}
