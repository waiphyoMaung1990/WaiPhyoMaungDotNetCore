using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiphyomaungDotNetCore.ConsoleApp.Models;

namespace WaiphyomaungDotNetCore.ConsoleApp.HttpClientExamples
{
    public class HttpClientExample
    {
        private string _blogEndpoint = "https://localhost:7282/api/blog";

        public async Task Run()
        {
            //await ReadAsync();
            //await EditAsync(5);
            ////await EditAsync(200);
            //await CreateAync("http Title", "http Author", "http content");
            //  await UpdateAsync(6, "httpUpdated title", "httpUpdated author", "httpUpdated content");
       //     await PatchAsync(7, "httpPatch title", "httpPatch author");
            await DeleteAsync(2);
            //await ReadAsync();
        }
        public async Task ReadAsync()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(_blogEndpoint);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                List<BlogDataModel> lst = JsonConvert.DeserializeObject<List<BlogDataModel>>(jsonStr)!;
                //Deserialize is used for to covert obtaining json object to .net object
                //if we want to tell null is not carry -use excl mark !

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
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{_blogEndpoint}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
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
                Console.WriteLine(await response.Content.ReadAsStringAsync());
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
            string jsonBlog = JsonConvert.SerializeObject(blog);
            //But post is only accept httpcontent, so we need to change tha jsonBlog to httpcontnet
            HttpContent httpContent = new StringContent(jsonBlog, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync(_blogEndpoint, httpContent);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

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
            // Assign parameter to getter/setter

            //This step is to c# object to json format for data exchange
            string jsonBlog = JsonConvert.SerializeObject(blog);


            //But  only accept httpcontent, so we need to change tha jsonBlog to httpcontnet
            HttpContent httpContent = new StringContent(jsonBlog, Encoding.UTF8, "application/json");

            // Construct the URL with the specific blog ID
            string _patchUrl = $"{_blogEndpoint}/{blog_id}";

            HttpClient client = new HttpClient();

            // Send the PUT request to update the blog
            HttpResponseMessage response = await client.PatchAsync(_patchUrl, httpContent);

            Console.WriteLine(await response.Content.ReadAsStringAsync());

        }

        public async Task DeleteAsync(int blog_id)
        {
            // Construct the URL with the specific blog ID
            string deleteUrl = $"{_blogEndpoint}/{blog_id}";


            HttpClient client = new HttpClient();

            // Send the DELETE request to remove the blog
            HttpResponseMessage response = await client.DeleteAsync(deleteUrl);

            // Output the response content to the console
            Console.WriteLine(await response.Content.ReadAsStringAsync());

        }

    }


}

