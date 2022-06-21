using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class MemberController : Controller
    {
        private readonly DataContext _context;

        public MemberController(DataContext context)
        {
            _context = context;
        }

        // Get section 

        [HttpGet]
        public IActionResult Index()
        {
            var courses = _context.Member.ToList();
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
            var course = _context.Member.Where(x => x.ID == id).FirstOrDefault();
            return View(course);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var course = _context.Member.Where(x => x.ID == id).FirstOrDefault();
            return View(course);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var course = _context.Member.Where(x => x.ID == id).FirstOrDefault();
            return View(course);
        }

   //  Post Section

       [HttpPost]
        public IActionResult Create(Member model)
        {
            _context.Member.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(Member model)
        {
            _context.Member.Update(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(Member model)
        {
            _context.Member.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
