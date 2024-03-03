using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.AppConfig;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiPhyoMaungDotNetCore.RealTimeChartUsingSignalR.Models;


namespace WaiPhyoMaungDontNetCore.RealTimeChartUsingSignalR
{
    public class AppDbContext:DbContext
    {
      public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<TeamDataModel> Teams { get; set; }
    }
}
