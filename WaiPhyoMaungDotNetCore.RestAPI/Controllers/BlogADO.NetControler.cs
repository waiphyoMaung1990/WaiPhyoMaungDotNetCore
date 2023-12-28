using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using WaiphyomaungDotnetCore.RestAPI.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WaiphyomaungDotnetCore.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogADO : ControllerBase
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
            string query = @"SELECT [Blog_Id]
      ,[Blog_Title]
      ,[Blog_Author]
      ,[Blog_Content]
  FROM [dbo].[Tbl_Blog]"
            ;
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ToString());
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();
            var blogs = MapDataTableToBlogs(dt);
         
            return Ok(blogs);

        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = @"SELECT [Blog_Id]
                          ,[Blog_Title]
                          ,[Blog_Author]
                          ,[Blog_Content]
                      FROM [dbo].[Tbl_Blog] WHERE Blog_Id = @Blog_Id";

            using (SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ToString()))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameter to the query
                    command.Parameters.AddWithValue("@Blog_Id", id);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Check if any rows were returned
                        if (dt.Rows.Count > 0)
                        {

                            BlogDataModel blog = MapDataTableToBlogs(dt)[0];
                            return Ok(blog);
                        }
                        else
                        {
                            return NotFound(); // If no blog with the specified id is found
                        }
                    }
                }
            }
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogDataModel blog)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ToString());
            connection.Open();

            string query = @"
INSERT INTO [dbo].[Tbl_Blog]
           ([Blog_Title]
           ,[Blog_Author]
           ,[Blog_Content])
     VALUES
           (@Blog_Title,@Blog_Author,@Blog_Content)";

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
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Title", blog.Blog_Title);
            command.Parameters.AddWithValue("@Blog_Author", blog.Blog_Author);
            command.Parameters.AddWithValue("@Blog_Content", blog.Blog_Content);//to prevent sql injection

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            //old style
            int result = command.ExecuteNonQuery();
            string message = result > 0 ? "Saving Successful." : "Saving Failed";
            return Ok(message);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogDataModel blog)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ToString()))
                {
                    connection.Open();
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
                    string query = @"
                UPDATE [dbo].[Tbl_Blog]
                SET [Blog_Title] = @Blog_Title
                    ,[Blog_Author] = @Blog_Author
                    ,[Blog_Content] = @Blog_Content
                WHERE Blog_Id = @Blog_Id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Blog_Id", id);
                        command.Parameters.AddWithValue("@Blog_Title", blog.Blog_Title);
                        command.Parameters.AddWithValue("@Blog_Author", blog.Blog_Author);
                        command.Parameters.AddWithValue("@Blog_Content", blog.Blog_Content);

                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            return Ok("Updating Successful.");
                        }
                        else
                        {
                            return NotFound("Blog not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it according to your application's needs
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogDataModel blog)
        {
            try
            {
                // Check if the blog with the specified id exists
                var existingBlog = GetBlogById(id);

                if (existingBlog == null)
                {
                    return NotFound(); // If no blog with the specified id is found
                }

                // Update only the non-null properties of the existing blog with the new values
                if (!string.IsNullOrEmpty(blog.Blog_Title))
                {
                    existingBlog.Blog_Title = blog.Blog_Title;
                }

                if (!string.IsNullOrEmpty(blog.Blog_Author))
                {
                    existingBlog.Blog_Author = blog.Blog_Author;
                }

                if (!string.IsNullOrEmpty(blog.Blog_Content))
                {
                    existingBlog.Blog_Content = blog.Blog_Content;
                }

                // Update the blog in the database using ADO.NET
                UpdateBlog(existingBlog);

                return Ok("Blog updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }



        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ToString());
            connection.Open();


            string query = @"
DELETE FROM [dbo].[Tbl_Blog]
      WHERE Blog_Id=@Blog_Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Id", id);

            int result = command.ExecuteNonQuery();
            string message = result > 0 ? "Deleting Successful." : "Deleting Failed";
            connection.Close();
            return Ok(message);
        }


        //-----------------------------------
        #region Helper Methods

        private List<BlogDataModel> MapDataTableToBlogs(DataTable dataTable)
        {
            var blogs = new List<BlogDataModel>();

            foreach (DataRow row in dataTable.Rows)
            {
                var blog = new BlogDataModel
                {
                    Blog_Id = Convert.ToInt32(row["Blog_Id"]),
                    Blog_Title = row["Blog_Title"].ToString(),
                    Blog_Author = row["Blog_Author"].ToString(),
                    Blog_Content = row["Blog_Content"].ToString()
                };

                blogs.Add(blog);
            }

            return blogs;
        }
        //
        private void UpdateBlog(BlogDataModel blog)
        {
            string query = @"
                UPDATE [dbo].[Tbl_Blog]
                SET [Blog_Title] = @Blog_Title,
                    [Blog_Author] = @Blog_Author,
                    [Blog_Content] = @Blog_Content
                WHERE [Blog_Id] = @Blog_Id";

            using (SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ToString()))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Blog_Id", blog.Blog_Id);
                    command.Parameters.AddWithValue("@Blog_Title", blog.Blog_Title);
                    command.Parameters.AddWithValue("@Blog_Author", blog.Blog_Author);
                    command.Parameters.AddWithValue("@Blog_Content", blog.Blog_Content);

                    command.ExecuteNonQuery();
                }
            }
        }

        private BlogDataModel? GetBlogById(int id)
        {
            string query = @"SELECT [Blog_Id]
                          ,[Blog_Title]
                          ,[Blog_Author]
                          ,[Blog_Content]
                      FROM [dbo].[Tbl_Blog] WHERE Blog_Id = @Blog_Id";

            using (SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ToString()))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Blog_Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapDataReaderToBlog(reader);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        private BlogDataModel MapDataReaderToBlog(SqlDataReader reader)
        {
            return new BlogDataModel
            {
                Blog_Id = Convert.ToInt32(reader["Blog_Id"]),
                Blog_Title = reader["Blog_Title"].ToString(),
                Blog_Author = reader["Blog_Author"].ToString(),
                Blog_Content = reader["Blog_Content"].ToString()
            };
        }

        #endregion
    }
}
