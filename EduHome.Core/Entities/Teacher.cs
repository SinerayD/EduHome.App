using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.ComponentModel;
using EduHomeCore.Entities;

namespace EduHome.Core.Entities
{
    public class Teacher :BaseModel
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(30)]
        [Display(Name = "Teacher FullName")]
        public string FullName { get; set; }
        public string? Image { get; set; }
        public string Title { get; set; }
        public string Links { get; set; }   

        public int PositionId { get; set; }
        public Position? Position { get; set; }
        [NotMapped]
        public IFormFile? FormFile { get; set; }
    }
}
