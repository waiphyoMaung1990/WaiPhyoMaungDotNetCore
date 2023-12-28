using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiphyomaungDotNetCore.ConsoleApp.Models;

namespace WaiphyomaungDotNetCore.ConsoleApp.EFCoreExamples
{
    public class EFCoreExample
    {
        private readonly AppDbContext _dbContext = new AppDbContext();
        public void Run()
        {
            // Read();
          //  Edit(2);
          //  Edit(100);
           // Create("EFCoretitle", "EFCoreauthor", "EFCorecontent");
            Update(1, "upEfftitle", "updateEffauthor", "updateEffcontent");
            Delete(16);
        }
        public void Read()
        {
            var lst = _dbContext.Blogs.AsNoTracking().ToList();
            //
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
            // (same)  BlogDataModel? item = db.Query<BlogDataModel>(query,new {Blog_Id=id}).FirstOrDefault();
            BlogDataModel? item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                Console.WriteLine("No Data found");
                return;
            }
            Console.WriteLine(item.Blog_Id);
            Console.WriteLine(item.Blog_Title);
            Console.WriteLine(item.Blog_Author);
            Console.WriteLine(item.Blog_Content);
        }
        private void Create(string title, string author, string content)
        {


            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };
            _dbContext.Blogs.Add(blog);

            int result = _dbContext.SaveChanges();
            string message = result > 0 ? "Saving Successful." : "Saving Failed";

            Console.WriteLine(message);

        }
        private void Update(int id, string title, string author, string content)
        {

            BlogDataModel? item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                Console.WriteLine("No Data found");
                return;
            }
            item.Blog_Title = title;
            item.Blog_Author = author;
            item.Blog_Content=content;

            int result = _dbContext.SaveChanges();
            string message = result > 0 ? "Updating Successful." : "Updating Failed";

            Console.WriteLine(message);

        }
        private void Delete(int id)
        {
            BlogDataModel? item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                Console.WriteLine("No Data found");
                return;
            }
            _dbContext.Blogs.Remove(item);

            int result = _dbContext.SaveChanges();
            string message = result > 0 ? "Deleting Successful." : "Deleting Failed";

            Console.WriteLine(message);

        }
    }
}
