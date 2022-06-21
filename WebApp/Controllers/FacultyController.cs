using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class FacultyController : Controller
    {
        private readonly DataContext _context;

        public FacultyController(DataContext context)
        {
            _context = context;
        }
    
        // Get section 
       
        [HttpGet]
        public IActionResult Index()
        {
            var courses = _context.Faculty.ToList();

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
            var course = _context.Faculty.Where(x => x.ID == id).FirstOrDefault();
            return View(course);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var course = _context.Faculty.Where(x => x.ID == id).FirstOrDefault();
            return View(course);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var course = _context.Faculty.Where(x => x.ID == id).FirstOrDefault();
            return View(course);
        }
        
        // Post Section 
       
        [HttpPost]
        public IActionResult Create(Faculty model)
        {
            _context.Faculty.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(Faculty model)
        {
            _context.Faculty.Update(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(Faculty model)
        {
            _context.Faculty.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
