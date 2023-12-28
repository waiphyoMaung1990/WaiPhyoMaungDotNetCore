using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.AppConfig;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiphyomaungDotNetCore.ConsoleApp.Models;

namespace WaiphyomaungDotNetCore.ConsoleApp.EFCoreExamples
{
    public class AppDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "WaiPhyoMaung_A",
                UserID = "sa",
                Password = "091537",
                TrustServerCertificate = true
            };
            optionsBuilder.UseSqlServer(_sqlConnectionStringBuilder.ConnectionString);

        }
        //Blogs==Table Name and Sql tbl name must unique
        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
