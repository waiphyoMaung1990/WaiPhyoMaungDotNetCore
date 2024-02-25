using Microsoft.EntityFrameworkCore;
using WaiPhyoMaungRepositoryPattern.Models;

namespace WaiPhyoMaungRepositoryPattern.Data
{
    public class EmployeeContext:DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) { }
        
        //for model
        public DbSet<Employee> Employees { get; set; }
    }
}
