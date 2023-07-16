using EduHomeCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EduHome.Core.Entities
{
    public class Position:BaseModel
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(30)]
        [Display(Name = "Position Name")]
        public string Name { get; set; }
        [Key]
        public List<Teacher>? Teachers { get; set; }
    }
}
