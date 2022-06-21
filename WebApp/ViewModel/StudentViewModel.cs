
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModel
{
    public class StudentViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Enrolled { get; set; }

        public IList<SelectListItem> Courses { get; set; }
}
}
