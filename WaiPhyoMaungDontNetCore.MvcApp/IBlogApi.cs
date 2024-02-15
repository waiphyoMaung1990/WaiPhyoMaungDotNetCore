using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiPhyoMaungDontNetCore.MvcApp.Models;

namespace WaiPhyoMaungDontNetCore.MvcApp
{
    //https://github.com/reactiveui/refit#readme

    public interface IBlogApi
    {
        //dont need to add domain uri ,only need to add endpoint
        [Get("/api/blog")]
        Task<List<BlogDataModel>> GetBlogs();


        [Get("/api/blog/{id}")]
        Task<BlogDataModel> GetBlog(int id);


        [Post("/api/blog")]
        Task<string> CreateBlog(BlogDataModel blog);

        [Put("/api/blog/{id}")]
        Task<string> UpdateBlog(int id, BlogDataModel blog);

        [Delete("/api/blog/{id}")]
        Task<string> DeleteBlog(int id);

    }
}
