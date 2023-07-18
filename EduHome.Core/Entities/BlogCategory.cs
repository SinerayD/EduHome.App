using EduHomeCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EduHome.Core.Entities
{
    public class BlogCategory:BaseModel
    {
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        [Display(Name = "Blog")]
        public int BlogId { get; set; }

        public Blog? Blog { get; set; }
    }
}
