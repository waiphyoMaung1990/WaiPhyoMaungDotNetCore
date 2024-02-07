using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.AppConfig;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WaiPhyoMaungRestAPI.ATM.Models;

namespace WaiPhyoMaungRestAPI.ATM.Models
{
    public class AppDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "ATMDb",
                UserID = "sa",
                Password = "091537",
                TrustServerCertificate = true
            };
            optionsBuilder.UseSqlServer(_sqlConnectionStringBuilder.ConnectionString);

        }
        //Blogs==Table Name and Sql tbl name must unique
        public DbSet<UserModel> User { get; set; }
     
    }
}
