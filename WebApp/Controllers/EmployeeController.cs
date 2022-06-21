using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DataContext _context;

        public EmployeeController(DataContext context)
        {
            _context = context;
        }

        // Get section 

        [HttpGet]
        public IActionResult Index()
        {
            var courses = _context.Employee.ToList();

            return View(courses);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var course = _context.Employee.Where(x => x.ID == id).FirstOrDefault();
            return View(course);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var course = _context.Employee.Where(x => x.ID == id).FirstOrDefault();
            return View(course);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var course = _context.Employee.Where(x => x.ID == id).FirstOrDefault();
            return View(course);
        }

        // Post Section 

        [HttpPost]
        public IActionResult Create(Employee model)
        {
            _context.Employee.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(Employee model)
        {
            _context.Employee.Update(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(Employee model)
        {
            _context.Employee.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}