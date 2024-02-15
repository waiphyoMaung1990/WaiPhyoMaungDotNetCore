using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Text;
using WaiPhyoMaungDontNetCore.MvcApp.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WaiPhyoMaungDontNetCore.MvcApp.Controllers
{
    public class BlogRestClientController : Controller
    {
        private readonly RestClient _restClient;

        public BlogRestClientController(RestClient restClient)
        {
            _restClient = restClient;
        }

        #region Get
        public async Task<IActionResult> Index()
        {
            try
            {
                List<BlogDataModel> lst = new();
                //From RestAPI 
                //Blog is controller name of RestApi
                RestRequest restRequest = new RestRequest(resource: "/api/Blog", Method.Get);
                var response = await _restClient.ExecuteAsync(restRequest);
                if (response.IsSuccessStatusCode)
                {
                    string jsonStr = response.Content!;
                    lst = JsonConvert.DeserializeObject<List<BlogDataModel>>(jsonStr)!;
                    lst = lst.OrderByDescending(blog => blog.Blog_Id).ToList();
                }
                return View(lst);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Create page
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        #endregion

        #region Save
        public async Task<IActionResult> Save(BlogDataModel model)
        {
            try
            {
                RestRequest request = new("/api/Blog", Method.Post);
                request.AddJsonBody(model);
                var response = await _restClient.ExecuteAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    await Console.Out.WriteLineAsync(response.Content!);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                BlogDataModel item = new BlogDataModel();
                RestRequest request = new RestRequest($"/api/Blog/{id}", Method.Get);
                var response = await _restClient.ExecuteAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string jsonStr = response.Content!;
                     item = JsonConvert.DeserializeObject<BlogDataModel>(jsonStr)!;
                }
                else
                {
                    Console.WriteLine(response.Content);
                }


                return View(item);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Update
        [HttpPost]
        public async Task<IActionResult> Update(int id, BlogDataModel model)
        {
            try
            {
                RestRequest request = new RestRequest($"/api/Blog/{id}", Method.Put);
                request.AddJsonBody(model);
               // request.AddBody(model);
                var response = await _restClient.ExecuteAsync(request);
                return RedirectToAction("Index");
                if (response.IsSuccessStatusCode)
                {
                    await Console.Out.WriteLineAsync(response.Content!);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return View("Error");
            }
        }
        #endregion

        #region Delete
        [HttpGet]
        
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                RestRequest request = new RestRequest($"/api/Blog/{id}", Method.Delete);

                var response = await _restClient.ExecuteAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Blog post deleted successfully.";
                    // Add some debugging output
                    Console.WriteLine("Message set successfully");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
    #endregion

}