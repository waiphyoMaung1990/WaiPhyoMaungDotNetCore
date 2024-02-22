using Microsoft.AspNetCore.Mvc;
using WaiphyomaungDotnetCore.MinimalAPI;
using Microsoft.EntityFrameworkCore;
using WaiphyomaungDotnetCore.MinimalAPI.Models;
namespace WaiPhyoMaungDotNetCore.MinimalAPI.Features.Blog
{
    public static class BlogServices
    {
        public static IEndpointRouteBuilder UseBlogService(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/blog/{pageNo}/{pageSize}", async ([FromServicesAttribute] AppDbContext dbContext, int pageNo, int pageSize) =>
            {
                return await dbContext.Blogs
                    .AsNoTracking()
                    .OrderByDescending(x => x.Blog_Id)
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            })
                .WithName("GetBlogs")
                .WithOpenApi();

            app.MapGet("/api/blog/{id}", async ([FromServicesAttribute] AppDbContext dbContext, int id) =>
            {
                var item = await dbContext.Blogs
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Blog_Id == id);
                if (item is null)
                {
                    return Results.NotFound("No data found.");
                }

                return Results.Ok(item);
            })
                .WithName("GetBlog")
                .WithOpenApi();

            app.MapPost("/api/blog", async ([FromServicesAttribute] AppDbContext dbContext, [FromBody] BlogDataModel blog) =>
            {
                await dbContext.Blogs.AddAsync(blog);
                var result = await dbContext.SaveChangesAsync();
                var message = result > 0 ? "Saving Successful." : "Saving Failed";
                return Results.Ok(message);
            })
                .WithName("CreateBlog")
                .WithOpenApi();

            app.MapPut("/api/blog/{id}", async ([FromServices] AppDbContext dbContext, int id, [FromBody] BlogDataModel updatedBlog) =>
            {
                var existingBlog = await dbContext.Blogs.FindAsync(id);
                if (existingBlog == null)
                {
                    return Results.NotFound("Blog not found.");
                }

                existingBlog.Blog_Author = updatedBlog.Blog_Author;
                existingBlog.Blog_Content = updatedBlog.Blog_Content;
                existingBlog.Blog_Title = updatedBlog.Blog_Title;


                dbContext.Blogs.Update(existingBlog);
                await dbContext.SaveChangesAsync();

                return Results.Ok("Blog updated successfully.");
            })
.WithName("UpdateBlog")
.WithOpenApi();

            app.MapPatch("/api/blog/{id}", async ([FromServices] AppDbContext dbContext, int id, [FromBody] BlogDataModel blog) =>
            {
                var item = await dbContext.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);
                if (item == null)
                {
                    return Results.NotFound("No data found.");
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

                dbContext.Blogs.Update(item);
                await dbContext.SaveChangesAsync();

                return Results.Ok("Blog patched successfully.");
            })
 .WithName("PatchBlog")
 .WithOpenApi();



            app.MapDelete("/api/blog/{id}", async ([FromServices] AppDbContext dbContext, int id) =>
            {
                var blog = await dbContext.Blogs.FindAsync(id);
                if (blog == null)
                {
                    return Results.NotFound("Blog not found.");
                }

                dbContext.Blogs.Remove(blog);
                await dbContext.SaveChangesAsync();

                return Results.Ok("Blog deleted successfully.");
            })
            .WithName("DeleteBlog")
            .WithOpenApi();


            return app;
        }
    }
}

