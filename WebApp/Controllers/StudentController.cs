using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Models;
using WebApp.ViewModel;

namespace WebApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly DataContext _context;

        public StudentController(DataContext context)
        {
            _context = context;
        }

        // Get section

        [HttpGet]
        public IActionResult Index()
        {
            var student = _context.Student.ToList();

            return View(student);
        }
        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}
        [HttpGet]
        public IActionResult Create()
        {
            var courses = _context.Course.Select(x => new SelectListItem()
            {
                Text = x.Title,
                Value = x.ID.ToString()
            }).ToList();
            StudentViewModel vm = new StudentViewModel();
            vm.Courses = courses;
            return View(vm);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var course = _context.Student.Where(x => x.ID == id).FirstOrDefault();
            return View(course);
        }
        //[HttpGet]
        //public IActionResult Edit(int id)
        //{
        //    var course = _context.Student.Where(x => x.ID == id).FirstOrDefault();
        //    return View(course);
        //}
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //var course = await _context.Student.Include(x => x.Enrollment).Where(y => y.ID == id).FirstOrDefault();
            //var Student = await _context.Student.Include(x => x.Enrollment).Where(y => y.ID == id).FirstOrDefaultAsync();
            var Student = await _context.Student.Where(y => y.ID == id).FirstOrDefaultAsync();
            //var selectedIds = await student.Enrollment.Select(x => x.CourseID).ToList();
            //var selectedIds = Student.Enrollment.Select(x => x.CourseID).ToList();
            //var items = _context.Course.Select(x => new SelectListItem()
            //{
            //    Text = x.Title,
            //    Value = x.ID.ToString(),
            //    Selected = selectedIds.Contains(x.ID)
            //});
            var selectedIds = Student.Enrollment.Select(x => x.CourseID).ToList();
            var courses = _context.Course.Select(x => new SelectListItem()
            {
                Text = x.Title,
                Value = x.ID.ToString(),
                Selected= selectedIds.Contains(x.ID)
            }).ToList();

            StudentViewModel vm = new StudentViewModel();
            vm.Courses = courses;
            return View(vm);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var course = _context.Student.Where(x => x.ID == id).FirstOrDefault();
            return View(course);
        }

        // Post Section 

        //[HttpPost]
        //public IActionResult Create(Student model)
        //{
        //    _context.Student.Add(model);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public IActionResult Create(StudentViewModel vm)
        {
            var student = new Student
            {
                Name = vm.Name,
                Enrolled = vm.Enrolled
            };
            var selectedCourses = vm.Courses.Where(x => x.Selected).Select(y => y.Value).ToList();
            foreach (var item in selectedCourses)
            //{
            //    student.Enrollment.Add(new StudentCourse()
            //    {
            //        CourseID = int.Parse(item)
            //    }
            //    );
            //}
            _context.Student.Add(student);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //public IActionResult Edit(Student model)
        //{
        //    _context.Student.Update(model);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        [HttpPost]
        public IActionResult Edit(StudentViewModel vm)
        {
            var student = _context.Student.Find(vm.ID);
            {
                student.Name = vm.Name;
                student.Enrolled = vm.Enrolled;
            };
            var studentById = _context.Student.Include(x => x.Enrollment).FirstOrDefault(y => y.ID == vm.ID);
            var existingIds = studentById.Enrollment.Select(x => x.CourseID).ToList();
            var selectedIds = vm.Courses.Where(x => x.Selected).Select(y => y.Value).Select(int.Parse).ToList();
            var toAdd = selectedIds.Except(existingIds);
            var toRemove = existingIds.Except(selectedIds);
            student.Enrollment = student.Enrollment.Where(x => !toRemove.Contains(x.CourseID)).ToList();

            foreach (var item in toAdd)
            {
                student.Enrollment.Add(new StudentCourse()
                {
                    CourseID = item
                });
            }
                _context.Student.Update(student);
                _context.SaveChanges();
                return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(Student model)
        {
            _context.Student.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
