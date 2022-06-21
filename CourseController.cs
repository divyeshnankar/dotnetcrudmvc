using EngLearn.Models;
using EngLearn.Models.CourseModule;
using EngLearn.Repositories.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EngLearn.Controllers.ApplicationController
{
    [Route("api/course")]
    //[Authorize(Roles ="Admin")]
    //[Authorize(Roles ="User")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private IHostingEnvironment _Environment;
        private ApplicationDbContext _Context;
        private ICourse _iCourse;

        public CourseController(IHostingEnvironment environment, ApplicationDbContext Context, ICourse iCourse)
        {
            _Environment = environment;
            _Context = Context;
            _iCourse = iCourse;
        }

        [HttpGet]
        [Route("getcourselist")]
        public IEnumerable<CourseViewModel> GetCourseList([FromQuery] Pagination pagination, string Search = "", string devicetype = "website", string email="")
        //public IEnumerable<CourseViewModel> GetCourseList(string devicetype = "website", string email="")
        {
            List<CourseViewModel> objlist = new List<CourseViewModel>();
            objlist = _iCourse.GetCourseList(pagination,Search,devicetype, email);
            return objlist;
        }
        [Route("getcoursebyid")]
        [HttpGet]
        public CourseViewModel GetCourseById(int id, string email = "", string devicetype = "website")
        {
            CourseViewModel obj = new CourseViewModel();
            obj = _iCourse.GetCourseById(id, email, devicetype);
            return obj;
        }
        //[HttpPost]
        //[Route("addcourse")]
        //public ResponseModel AddCourse(CourseViewModel CourseModelparams)
        //{
        //    ResponseModel obj = new ResponseModel();
        //    obj = _iCourse.AddCourse(CourseModelparams);
        //    return obj;
        //}
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        [Route("addcourse")]
        public async Task<ActionResult<ResponseModel>> AddCourse([FromForm] CourseViewModel CourseModelparams)
        {
            try
            {
                var file = CourseModelparams.ImageFile;
                if (CourseModelparams.ImageFile != null)
                {
                    //Create a Folder.
                    string contentPath = this._Environment.ContentRootPath;
                    string wwwPath = this._Environment.WebRootPath;

                    string path = Path.Combine(contentPath, "content", "Images", "Course");
                    //Create a Directory.
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), path);
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        CourseModelparams.Image = fileName;
                        var fullPath = Path.Combine(pathToSave, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                    }
                }
                var data = _iCourse.AddCourse(CourseModelparams);
                return data;
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error is retrieving Data from database" + Ex.Message.ToString());
            }
        }
        [HttpDelete]
        //[Authorize(Roles = "Admin")]
        [Route("deletecourse")]
        public ResponseModel RemoveCourse(int id)
        {
            ResponseModel obj = new ResponseModel();
            obj = _iCourse.RemoveCourse(id);
            return obj;
        }
        //[HttpPut]
        //[Route("updatecourse")]
        //public ResponseModel UpdateCourse(CourseModel CourseModelparams)
        //{
        //    ResponseModel obj = new ResponseModel();
        //    obj = _iCourse.UpdateCourse(CourseModelparams);
        //    return obj;
        //}
        [HttpPut]
       // [Authorize(Roles = "Admin")]
        [Route("updatecourse")]
        public async Task<ActionResult<ResponseModel>> UpdateCourse([FromForm] CourseViewModel CourseModelparams)
        {
            try
            {
                var file = CourseModelparams.ImageFile;
                if (CourseModelparams.ImageFile != null)
                {
                    //Create a Folder.
                    string contentPath = this._Environment.ContentRootPath;
                    string wwwPath = this._Environment.WebRootPath;

                    string path = Path.Combine(contentPath, "content", "Images", "Course");
                    //Create a Directory.
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), path);

                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        CourseModelparams.Image = fileName;
                        var fullPath = Path.Combine(pathToSave, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                    }
                }
                var data = _iCourse.UpdateCourse(CourseModelparams);
                return data;
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error is retrieving Data from database" + Ex.Message.ToString());
            }
         }
    }
}
