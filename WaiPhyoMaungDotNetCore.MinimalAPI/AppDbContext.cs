using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.AppConfig;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiphyomaungDotnetCore.MinimalAPI.Models;

namespace WaiphyomaungDotnetCore.MinimalAPI
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        //Blogs==Table Name and Sql tbl name must unique
        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
