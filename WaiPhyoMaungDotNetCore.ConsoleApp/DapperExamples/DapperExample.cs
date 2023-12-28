using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiphyomaungDotNetCore.ConsoleApp.Models;

namespace WaiphyomaungDotNetCore.ConsoleApp.DapperExamples
{
    public class DapperExample
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "WaiPhyoMaung_A",
            UserID = "sa",
            Password = "091537"
        };
        public void Run()
        {
            // Read();
            // Edit(100);
            //  Create("testtitle", "testauthor", "testcontent");
            //  Update(1,"updatetitle", "updateauthor", "updatecontent");
            Delete(15);
        }
        public void Read()
        {
            //old writing

            //          using (IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
            //          {
            //              string query = @"SELECT [Blog_Id]
            //    ,[Blog_Title]
            //    ,[Blog_Author]
            //    ,[Blog_Content]
            //FROM [dbo].[Tbl_Blog]";
            //              // var lst=db.Query(query).ToList();
            //              List<BlogDataModel> lst = db.Query<BlogDataModel>(query).ToList();
            //              foreach (BlogDataModel item in lst)
            //              {
            //                  Console.WriteLine(item.Blog_Id);
            //                  Console.WriteLine(item.Blog_Title);
            //                  Console.WriteLine(item.Blog_Author);
            //                  Console.WriteLine(item.Blog_Content);

            //              }
            //          }


            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);

            string query = @"SELECT [Blog_Id]
      ,[Blog_Title]
      ,[Blog_Author]
      ,[Blog_Content]
  FROM [dbo].[Tbl_Blog]";
            // var lst=db.Query(query).ToList();
            List<BlogDataModel> lst = db.Query<BlogDataModel>(query).ToList();
            foreach (BlogDataModel item in lst)
            {
                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);

            }
        }
        public void Edit(int id)
        {

            string query = @"SELECT [Blog_Id]
      ,[Blog_Title]
      ,[Blog_Author]
      ,[Blog_Content]
  FROM [dbo].[Tbl_Blog] where Blog_Id=@Blog_Id";
            // var lst=db.Query(query).ToList();
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            // (same)  BlogDataModel? item = db.Query<BlogDataModel>(query,new {Blog_Id=id}).FirstOrDefault();
            BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel { Blog_Id = id }).FirstOrDefault();
            if (item is null)
            {
                Console.WriteLine("No Data found");

            }
            Console.WriteLine(item.Blog_Id);
            Console.WriteLine(item.Blog_Title);
            Console.WriteLine(item.Blog_Author);
            Console.WriteLine(item.Blog_Content);

        }
        private void Create(string title, string author, string content)
        {

            string query = @"
INSERT INTO [dbo].[Tbl_Blog]
           ([Blog_Title]
           ,[Blog_Author]
           ,[Blog_Content]) 
     VALUES
           (@Blog_Title,@Blog_Author,@Blog_Content)";
            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);


            int result = db.Execute(query, blog);
            string message = result > 0 ? "Saving Successful." : "Saving Failed";

            Console.WriteLine(message);

        }
        private void Update(int id, string title, string author, string content)
        {

            string query = @"
UPDATE [dbo].[Tbl_Blog]
   SET [Blog_Title] = @Blog_Title
      ,[Blog_Author] = @Blog_Author
      ,[Blog_Content] =@Blog_Content
 WHERE Blog_Id=@Blog_Id";
            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Id = id,
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);


            int result = db.Execute(query, blog);
            string message = result > 0 ? "Updating Successful." : "Updating Failed";

            Console.WriteLine(message);

        }
        private void Delete(int id)
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

            Console.WriteLine(message);

        }
    }
}
