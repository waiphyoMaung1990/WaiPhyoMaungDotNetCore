using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaiPhyoMaungDontNetCore.MvcApp.Models;
using System.Collections.Generic; // Add this namespace for List<T>

namespace WaiPhyoMaungDontNetCore.MvcApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _appdbcontext;

        public BlogController(AppDbContext context)
        {
            _appdbcontext = context;
        }

        public async Task<IActionResult> Index()
        {
            List<BlogDataModel> blogs = await _appdbcontext.Blogs.OrderByDescending(x => x.Blog_Id).ToListAsync();
            return View(blogs); // Pass the list of blogs to the view
        }



        /*  #region pagination 
          //https://localhost:3000/blog/index?pageNo=1&pages=Size=10
          //ကစားရင် total သိရမယ်။
          //pageSize ဆိုတာ page တစ်ခုမှာ ပြချင်တဲ့ row ကိုပြောတာ,rowCountက စုစုပေါင်းးပြချင်တဲ့ pageတိုင်းက rowအားလုံးအရေအတွက်/pageCountက အဲ့ရှိတဲ့ rowCountတွေကို page ဘယ်နှစ်ခုခွဲပြမလဲဆိုတာကိုသတ်မှတ်တာ
          //total rowCount က၂၅/ page တစ်ခုမှာ pagesize ၅ခုပြချင်ရင်, pageCount=rowCount/pageSize= 0 အကြွင်းက ၀ ထက်ပိုရင် ၁ တိုး မပိုးရင် ၅ page(pageCount)
          public async Task<IActionResult> BlogList(int pageNo = 1, int pageSize = 10)
          {
              //Asnotracking မပြင်ခင်ကျန်ခဲ့တဲ့ data ကိုပြရန်
              var query = _appdbcontext.Blogs.AsNoTracking().OrderByDescending(x => x.Blog_Id).ToListAsync();

              //for 10 ကြောင်း
              var lst = await query
           .Skip((pageNo - 1) * pageSize)
           .Take(pageSize)
           .ToListAsync();


              //်for total
              var rowCount = await query.CountAsync();
              var pageCount = rowCount / pageSize;

              //total is 57 /page
              //size is  10= 5 ,modulus is 7 စားလို့ပျက်ရင် မတိုးဘူး အကြွင်းကျန်ရင် ၁ တိုး


              if (rowCount%pageSize>0)
              {
                  pageCount++;
              }


              BlogResponseModel model = new BlogResponseModel()
              {
                  Data = lst,
                  PageSetting = new PageSettingModel(pageNo, pageSize,pageCount,rowCount)
              };
              return View("BlogList", lst); // Pass the list of blogs to the view

              //page တွေအများကြီးဖြစ်နေရင် အတုံး၁၀၀ လုံးမပြဘဲ 1 2 3 4..6 7 8 9
              //1 to 40 block 
              //1 2 3 -38 39 40 one format
              //pageNo<=3 || pageNo-2 >=40

          }

          #endregion
        */

        /*   Viewမှာ list လို့သုံးထားလို့
           string GetLink(int pageNo, int pageSize)
           {
               return $"/Blog/List?pageNo={pageNo}&pageSize={pageSize}";
           }

           */
        [ActionName("List") ]
        public async Task<IActionResult> BlogList(int pageNo = 1, int pageSize = 10)


        {
            var query = _appdbcontext.Blogs.AsNoTracking().OrderByDescending(x => x.Blog_Id);

            var lst = await query
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var rowCount = await query.CountAsync();//total rowcount 
            var pageCount = rowCount / pageSize;//101/10

            if (rowCount % pageSize > 0)
            {
                pageCount++;
            }

            BlogResponseModel model = new BlogResponseModel()
            {
                Data = lst,
                PageSetting = new PageSettingModel(pageNo, pageSize, pageCount, rowCount)//1,10,11,101
            };

            return View("BlogList", model); // Pass the model to the view
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Save(BlogDataModel requestModel)
        {
            await _appdbcontext.Blogs.AddAsync(requestModel);
            await _appdbcontext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _appdbcontext.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);

            if (item == null)
            {
                return RedirectToAction("Index");
            }

            _appdbcontext.Remove(item);
            await _appdbcontext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int id)
        {
            var item = await _appdbcontext.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);

            if (item == null)
            {
                return RedirectToAction("Index");
            }
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, BlogDataModel requestModel)
        {
            var item = await _appdbcontext.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);

            if (item is null)
            {
                return RedirectToAction("Index");
            }

            item.Blog_Title = requestModel.Blog_Title;
            item.Blog_Author = requestModel.Blog_Author;
            item.Blog_Content = requestModel.Blog_Content;

            await _appdbcontext.SaveChangesAsync();

            return RedirectToAction("Index");
        }


    }
}
