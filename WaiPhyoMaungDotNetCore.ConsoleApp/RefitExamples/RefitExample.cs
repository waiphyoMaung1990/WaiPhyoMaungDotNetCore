using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiphyomaungDotNetCore.ConsoleApp.Models;

namespace WaiphyomaungDotNetCore.ConsoleApp.RefitExamples
{
    public class RefitExample
    {
        private readonly IBlogApi _blogApi = RestService.For<IBlogApi>("https://localhost:7282");
        public async Task Run()
        {
            //await Read();
            //await Edit(8);
            //await Edit(2);
           // await Create("Refit Title", "Refit Author", "Refit content");
            await Update(6, "Refit Titles", "Refit Authors", "Refit Contents");
            await Delete(11);
        }
        //[Get("/api/Blog")]
        // Task<List<BlogDataModel>> GetBlogs()
        public async Task Read()
        {

            var lst = await _blogApi.GetBlogs();
            foreach (BlogDataModel item in lst)
            {
                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);
            }
        }


        public async Task Edit(int id)
        {
            try
            {
                var item = await _blogApi.GetBlog(id);

                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);
            }
            catch (Refit.ApiException ex)
            {

                Console.WriteLine(ex.Content!.ToString());
                Console.WriteLine(ex.ReasonPhrase!.ToString());

            }


        }

        public async Task Create(string title, string author, string content)
        {
            var messsage = await _blogApi.CreateBlog(new BlogDataModel
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            });
            await Console.Out.WriteLineAsync(messsage);

        }

        public async Task Update(int id, string title, string author, string content)
        {
            try
            {
                var messsage = await _blogApi.UpdateBlog(id, new BlogDataModel
                {
                    Blog_Id = id,
                    Blog_Title = title,
                    Blog_Author = author,
                    Blog_Content = content
                });
                await Console.Out.WriteLineAsync(messsage);
            }
            catch (Refit.ApiException ex)
            {

                Console.WriteLine(ex.ReasonPhrase!.ToString());
            }



        }


        public async Task Delete(int id)
        {
            try
            {
                var messsage = await _blogApi.DeleteBlog(id);
                await Console.Out.WriteLineAsync(messsage);
            }
            catch (Refit.ApiException ex)
            {

                Console.WriteLine(ex.ReasonPhrase!.ToString());
            }


        }
    }
}
