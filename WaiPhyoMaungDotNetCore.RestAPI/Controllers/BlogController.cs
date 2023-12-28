using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WaiphyomaungDotnetCore.RestAPI.Models;

namespace WaiphyomaungDotnetCore.RestAPI.Controllers
{
    //http://localhost:5000/api/blog
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        AppDbContext appDbContext = new AppDbContext();


        [HttpGet]
        public IActionResult GetBlogs()
        {

            var lst = appDbContext.Blogs.ToList();

            return Ok(lst);
        }


        [HttpGet("{id}")]
        //if two parameter write with slash [HttpGet("{id}/{}")]
        public IActionResult GetBlog(int id)
        {
            var item = appDbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                return NotFound("No Data Found");
            }
            return Ok(item);
        }


        [HttpPost]
        public IActionResult CreateBlog(BlogDataModel blog)
        {
            appDbContext.Blogs.Add(blog);
            var result = appDbContext.SaveChanges();
            var message = result > 0 ? "Saving Successful." : "Saving Failed";
            return Ok(message);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogDataModel blog)
        {
            var item = appDbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                return NotFound("No Data Found");
            }
            if (string.IsNullOrEmpty(blog.Blog_Title))
            {
                return BadRequest("Blog_Title is required.");
            }
            if (string.IsNullOrEmpty(blog.Blog_Author))
            {
                return BadRequest("Blog_Author is required.");
            }
            if (string.IsNullOrEmpty(blog.Blog_Content))
            {
                return BadRequest("Blog_Content is required.");
            }

            item.Blog_Title = blog.Blog_Title;
            item.Blog_Author = blog.Blog_Author;
            item.Blog_Content = blog.Blog_Content;
            int result = appDbContext.SaveChanges();
            var message = result > 0 ? "Updating Successful." : "Updating Failed";
            return Ok(message);

        }


        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogDataModel blog)
        {
            var item = appDbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                return NotFound("No Data Found");
            }
            if (!string.IsNullOrEmpty(blog.Blog_Title))
            {
                item.Blog_Title = blog.Blog_Title;
            }
            if (!string.IsNullOrEmpty(blog.Blog_Author))
            {
                item.Blog_Author = blog.Blog_Author;
            }
            if (!string.IsNullOrEmpty(blog.Blog_Content))
            {
                item.Blog_Content = blog.Blog_Content;
            }


            int result = appDbContext.SaveChanges();
            var message = result > 0 ? "Updating Successful." : "Updating Failed";
            return Ok(message);

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var item = appDbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                return NotFound("No Data Found");
            }
            appDbContext.Blogs.Remove(item);
            int result = appDbContext.SaveChanges();
            var message = result > 0 ? "Deleting Successful." : "Deleting Failed";
            return Ok(message);
        }
    }
}
