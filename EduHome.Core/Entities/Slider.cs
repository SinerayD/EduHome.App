using EduHomeCore.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.Entities
{
    public class Slider : BaseModel
    {
       
        public string? Image { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Text is required")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Link is required")]
        public string Link { get; set; }
        public bool IsActive { get; set; }

        [NotMapped]
        public IFormFile? FormFile { get; set; }
    }
}