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
        public class Teacher : BaseModel
        {
            [Required(ErrorMessage = "Full Name is required")]
            [MaxLength(30)]
            [Display(Name = "Teacher Full Name")]
            public string FullName { get; set; }

            public string? Image { get; set; }

            [Required(ErrorMessage = "Email is required")]
            [EmailAddress(ErrorMessage = "Invalid Email Address")]
            public string Mail { get; set; }
            public string AboutMe { get; set; }

            public string? Skype { get; set; }

            [Display(Name = "Experience (in years)")]
            public int Experience { get; set; }

            [Phone(ErrorMessage = "Invalid Phone Number")]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }

            public string Faculty { get; set; }

            [Display(Name = "Position")]
            public int PositionId { get; set; }

            public Position? Position { get; set; }
            public int DegreeId { get; set; }
            public Degree? Degree { get; set; }
            public List<Skill>? Skills { get; set; }
            public List<Social>? Socials { get; set; }
            [NotMapped]
            public List<int>? HobbyIds { get; set; }
           public List<TeacherHobby>? TeacherHobbies { get; set; }

            [NotMapped]
            public IFormFile? FormFile { get; set; }

            
    }
    }

