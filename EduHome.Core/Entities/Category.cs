using EduHome.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHomeCore.Entities
{
    public class Category : BaseModel
    {
        [Required]
        public string? Name { get; set; }
        public List<CourseCategory>? CourseCategories { get; set; }
        public List<BlogCategory>? BlogCategories { get; set; }
    }
}
