using WaiPhyoMaungRepositoryPattern.Data;
using WaiPhyoMaungRepositoryPattern.Infrastructure;
using WaiPhyoMaungRepositoryPattern.Models;

namespace WaiPhyoMaungRepositoryPattern.Service
{
    public class EmployeeRepo : IEmployee
    {
        private readonly EmployeeContext _context;

        public EmployeeRepo(EmployeeContext context)
        {
            _context = context;
        }

        void IEmployee.Delete(Employee employee)
        {
            _context.Employees.Remove(employee);
        }

        List<Employee> IEmployee.GetAll()
        {
            return _context.Employees.ToList();
        }

        Employee IEmployee.GetById(int id)
        {
            return _context.Employees.Where(x => x.Id == id).FirstOrDefault()!;
        }

        void IEmployee.Insert(Employee employee)
        {
            _context.Employees.Add(employee);
        }

        void IEmployee.Save()
        {
            _context.SaveChanges();
        }

        void IEmployee.Update(Employee employee)
        {
            _context.Employees.Update(employee);
        }
    }
}
