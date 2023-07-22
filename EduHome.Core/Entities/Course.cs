using EduHomeCore.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EduHome.Core.Entities
{
    public class Course:BaseModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]

        public string AboutCourse { get; set; }
        [Required]

        public string Apply { get; set; }
        [Required]

        public string Certification { get; set; }
        [Required]

        public DateTime StartDate { get; set; }
        [Required]

        public double CourseDuration { get; set; }
        [Required]


        public double ClassDuration { get; set; }
        [Required]


        public string SkillLevel { get; set; }
        [Required]


        public int StudentCount { get; set; }
        [Required]

        public double CourseFee { get; set; }

        public string? Image { get; set; }

        [Required]
        public int CourseLanguageId { get; set; }

        [NotMapped]
        public List<int>? CategoryIds { get; set; }

        [NotMapped]
        public List<int>? TagIds { get; set; }
        public CourseLanguage? CourseLanguage { get; set; }

        public List<CourseCategory>? CourseCategories { get; set; }

        public List<CourseTag>? CourseTags { get; set; }

        [NotMapped]
        public IFormFile? FormFile { get; set; }
    }
}
