using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using WaiphyomaungDotNetCore.ConsoleApp.Models;

namespace WaiphyomaungDotNetCore.ConsoleApp.RestClientExamples
{
    public class RestClientExample
    {

        private string _blogEndpoint = "https://localhost:7282/api/blog";

        public async Task Run()
        {
            // await ReadAsync();
            // await EditAsync(7);
            // await EditAsync(200);
            //  await CreateAync("RestClient Title", "RestClient Author", "RestClient content");
            // await UpdateAsync(6, "RestClient title", "RestClient author", "RestClient content");
          //  await PatchAsync(7, "RestClient title", "RestClient author");
            await DeleteAsync(2);
            //await ReadAsync();
        }
        public async Task ReadAsync()
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest(_blogEndpoint, Method.Get);
            var response = await client.ExecuteAsync(request);
            //     var response = await client.GetAsync(request);


            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                List<BlogDataModel> lst = JsonConvert.DeserializeObject<List<BlogDataModel>>(jsonStr)!;
                foreach (BlogDataModel item in lst)
                {
                    Console.WriteLine(item.Blog_Id);
                    Console.WriteLine(item.Blog_Title);
                    Console.WriteLine(item.Blog_Author);
                    Console.WriteLine(item.Blog_Content);
                }
            }
        }



        public async Task EditAsync(int id)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"{_blogEndpoint}/{id}", Method.Get);
            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                BlogDataModel item = JsonConvert.DeserializeObject<BlogDataModel>(jsonStr)!;
                //Deserialize is used for to covert obtaining json object to .net object
                //if we want to tell null is not carry -use excl mark !

                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);

            }
            else
            {
                Console.WriteLine(response.Content);
            }
        }


        public async Task CreateAync(string title, string author, string content)
        {
            var blog = new BlogDataModel
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };
            // Assign parameter to getter/setter

            //This step is to c# object to json format for data exchange

            RestClient client = new RestClient();
            RestRequest request = new RestRequest(_blogEndpoint, Method.Post);
            request.AddJsonBody(blog);
            var response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content!);
        }


        public async Task UpdateAsync(int blog_id, string title, string author, string content)
        {
            var blog = new BlogDataModel
            {
                Blog_Id = blog_id,
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };
            // Assign parameter to getter/setter

            //This step is to c# object to json format for data exchange
            string jsonBlog = JsonConvert.SerializeObject(blog);


            //But  only accept httpcontent, so we need to change tha jsonBlog to httpcontnet
            HttpContent httpContent = new StringContent(jsonBlog, Encoding.UTF8, "application/json");

            // Construct the URL with the specific blog ID
            string _updateUrl = $"{_blogEndpoint}/{blog_id}";

            HttpClient client = new HttpClient();

            // Send the PUT request to update the blog
            HttpResponseMessage response = await client.PutAsync(_updateUrl, httpContent);

            Console.WriteLine(await response.Content.ReadAsStringAsync());

        }

        public async Task PatchAsync(int blog_id, string title, string author)
        {
            var blog = new BlogDataModel
            {
                Blog_Id = blog_id,
                Blog_Title = title,
                Blog_Author = author

            };
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"{_blogEndpoint}/{blog_id}", Method.Patch);
            request.AddBody(blog);
            var response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content!);

        }

        public async Task DeleteAsync(int blog_id)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"{_blogEndpoint}/{blog_id}", Method.Delete);
            var response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content!);

        }

    }


}

