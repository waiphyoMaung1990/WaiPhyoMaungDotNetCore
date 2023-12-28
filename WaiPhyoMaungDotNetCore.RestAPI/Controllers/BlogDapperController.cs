using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using WaiphyomaungDotnetCore.RestAPI.Models;

namespace WaiphyomaungDotnetCore.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "WaiPhyoMaung_A",
            UserID = "sa",
            Password = "091537",
            TrustServerCertificate = true
        };
        [HttpGet]
        public IActionResult GetBlogs()
        {
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            string query = @"SELECT [Blog_Id]
      ,[Blog_Title]
      ,[Blog_Author]
      ,[Blog_Content]
  FROM [dbo].[Tbl_Blog]";
            // var lst=db.Query(query).ToList();
            List<BlogDataModel> lst = db.Query<BlogDataModel>(query).ToList();
            return Ok(lst);
        }


        [HttpGet("{id}")]
        //if two parameter write with slash [HttpGet("{id}/{}")]
        public IActionResult GetBlog(int id)
        {
            string query = @"SELECT [Blog_Id]
      ,[Blog_Title]
      ,[Blog_Author]
      ,[Blog_Content]
  FROM [dbo].[Tbl_Blog] where Blog_Id=@Blog_Id";

            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel { Blog_Id = id }).FirstOrDefault();

            if (item is null)
            {
                return NotFound("No Data found");

            }
            return Ok(item);
        }


        [HttpPost]
        public IActionResult CreateBlog(BlogDataModel blog)
        {
            string query = @"
INSERT INTO [dbo].[Tbl_Blog]
           ([Blog_Title]
           ,[Blog_Author]
           ,[Blog_Content]) 
     VALUES
           (@Blog_Title,@Blog_Author,@Blog_Content)";
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);
            string message = result > 0 ? "Saving Successful." : "Saving Failed";
            return Ok(message);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogDataModel blog)
        {
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            #region getbyId
            string query = @"SELECT [Blog_Id]
      ,[Blog_Title]
      ,[Blog_Author]
      ,[Blog_Content]
  FROM [dbo].[Tbl_Blog] where Blog_Id=@Blog_Id";

            BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel { Blog_Id = id }).FirstOrDefault();

            if (item is null)
            {
                return NotFound("No Data found");

            }
            #endregion

            #region check required fields
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
            #endregion
            string queryUpdate = @"
UPDATE [dbo].[Tbl_Blog]
   SET [Blog_Title] = @Blog_Title
      ,[Blog_Author] = @Blog_Author
      ,[Blog_Content] =@Blog_Content
 WHERE Blog_Id=@Blog_Id";

            int result = db.Execute(queryUpdate, blog);
            string message = result > 0 ? "Updating Successful." : "Updating Failed";
            return Ok(message);

        }


        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogDataModel blog)
        {
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            #region getbyId
            string query = @"SELECT [Blog_Id]
      ,[Blog_Title]
      ,[Blog_Author]
      ,[Blog_Content]
  FROM [dbo].[Tbl_Blog] where Blog_Id=@Blog_Id";

            BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel { Blog_Id = id }).FirstOrDefault();

            if (item is null)
            {
                return NotFound("No Data found");

            }
            #endregion

            string conditions = string.Empty;
            if (!string.IsNullOrEmpty(blog.Blog_Title))
            {
                conditions += @" [Blog_Title] = @Blog_Title, ";
            }
            if (!string.IsNullOrEmpty(blog.Blog_Author))
            {
                conditions += @" [Blog_Author] = @Blog_Author, ";
            }
            if (!string.IsNullOrEmpty(blog.Blog_Content))
            {
                conditions += @" [Blog_Content] = @Blog_Content, ";
            }
            // check for incoming data is null or not
            if (conditions.Length == 0)
            {
                return BadRequest("Invalid Request.");
            }
            conditions = conditions.Substring(0, conditions.Length - 2);
            blog.Blog_Id= id;
            string queryUpdate = $@"
UPDATE [dbo].[Tbl_Blog]
   SET {conditions}
 WHERE Blog_Id=@Blog_Id";

            int result = db.Execute(queryUpdate, blog);
            string message = result > 0 ? "Updating Successful." : "Updating Failed";
            return Ok(message);

        }


        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            string query = @"
DELETE FROM [dbo].[Tbl_Blog]
      WHERE Blog_Id=@Blog_Id";
            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Id = id,

            };
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);


            int result = db.Execute(query, blog);
            string message = result > 0 ? "Deleting Successful." : "Deleting Failed";
            return Ok(message);
        }

    }
}
