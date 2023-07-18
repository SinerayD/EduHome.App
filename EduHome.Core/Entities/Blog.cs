using EduHomeCore.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EduHome.Core.Entities
{
    public class Blog:BaseModel
    {
            public int Id { get; set; }

            [Required(ErrorMessage = "Title is required")]
            public string Title { get; set; }

            public string Description { get; set; }

            public string Author { get; set; }

            public string Image { get; set; }

            [NotMapped]
            public List<int> CategoryIds { get; set; }

            [NotMapped]
            public List<int> TagIds { get; set; }

            [NotMapped]
            public List<BlogCategory>? BlogCategories { get; set; }

            [NotMapped]
            public List<BlogTag>? BlogTags { get; set; }

            [NotMapped]
            [Display(Name = "Blog Image")]
            [Required(ErrorMessage = "Please choose an image")]
            public IFormFile? FormFile { get; set; }
     
    }
}
