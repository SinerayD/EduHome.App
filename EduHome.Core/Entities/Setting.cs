using EduHome.Core.Entities;
using EduHomeCore.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduHome.Core.Entities
{
    public class Setting : BaseModel
    {
        [Required(ErrorMessage = "About Title is required")]
        public string AboutTitle { get; set; }

        [Required(ErrorMessage = "About Text is required")]
        public string AboutText { get; set; }
        public string AboutImage { get; set; }

        [Required(ErrorMessage = "Video Link is required")]
        public string VideoLink { get; set; }

        [Required(ErrorMessage = "Header Logo is required")]
        public string HeaderLogo { get; set; }

        [Required(ErrorMessage = "Footer Logo is required")]
        public string FooterLogo { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        public int PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [NotMapped]
        [Display(Name = "Course Image")]
        [Required(ErrorMessage = "Please choose an image")]
        public IFormFile? FormFile { get; set; }
    }
}
