using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.AppConfig;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiPhyoMaungDontNetCore.MvcApp.Models;


namespace WaiPhyoMaungDontNetCore.MvcApp
{
    public class AppDbContext:DbContext
    {
      public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
