using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WaiPhyoMaungRepositoryPattern.Infrastructure;
using WaiPhyoMaungRepositoryPattern.Models;

namespace WaiPhyoMaungRepositoryPattern.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployee _employee;

        public HomeController(IEmployee employee)
        {
            _employee = employee;
        }

        // Retrieve all employees and display in the Index view
        public IActionResult Index()
        {
            var employees = _employee.GetAll();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Handle form submission to create a new employee
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            _employee.Insert(employee);
            _employee.Save();
            return RedirectToAction("Index");
        }


       
        public IActionResult Details(int id)
        {
            var employee = _employee.GetById(id);
            return View(employee);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _employee.GetById(id);
            return View(employee);
        }

        // Handle form submission to update an existing employee
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            _employee.Update(employee);
            _employee.Save();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var employee = _employee.GetById(id);
            return View(employee);
        }

        // Handle form submission to delete an employee
        [HttpPost]
        public IActionResult Delete(Employee employee)
        {
           
            _employee.Delete(employee);
            _employee.Save();
            return RedirectToAction("Index");
        }

        // Privacy page action
        public IActionResult Privacy()
        {
            return View();
        }

        // Error handling
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
