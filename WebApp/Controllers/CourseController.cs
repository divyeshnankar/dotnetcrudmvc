using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CourseController : Controller
    {
        private readonly DataContext _context;

        public CourseController(DataContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Get section of Course Model
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            var courses = _context.Course.ToList();
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
            var course = _context.Course.Where(x => x.ID == id).FirstOrDefault();
            return View(course);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var course = _context.Course.Where(x => x.ID == id).FirstOrDefault();
            return View(course);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var course = _context.Course.Where(x => x.ID == id).FirstOrDefault();
            return View(course);
        }
        /// <summary>
        /// Post Section of Course Model
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(Course model)
        {
            _context.Course.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(Course model)
        {
            _context.Course.Update(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(Course model)
        {
            _context.Course.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
