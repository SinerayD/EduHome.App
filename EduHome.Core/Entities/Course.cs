using EduHomeCore.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace EduHome.Core.Entities
{
    public class Course:BaseModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string Description { get; set; }

        public string AboutCourse { get; set; }

        public string Apply { get; set; }

        public string Certification { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Course Duration is required")]
        public double CourseDuration { get; set; }

        [Required(ErrorMessage = "Class Duration is required")]
        public double ClassDuration { get; set; }

        public string SkillLevel { get; set; }

        [Required(ErrorMessage = "Student Count is required")]
        public int StudentCount { get; set; }
        public string Assessment { get; set; }  

        [Required(ErrorMessage = "Course Fee is required")]
        public double CourseFee { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "Course Assets ID is required")]
        public int CourseAssetsId { get; set; }

        [Required(ErrorMessage = "Course Language ID is required")]
        public int CourseLanguageId { get; set; }

        [NotMapped]
        public List<int> CategoryIds { get; set; }

        [NotMapped]
        public List<int> TagIds { get; set; }

        public CourseAssets? CourseAssets { get; set; }

        public CourseLanguage? CourseLanguage { get; set; }

        [NotMapped]
        public List<CourseCategory>? CourseCategories { get; set; }

        [NotMapped]
        public List<CourseTag>? CourseTags { get; set; }

        [NotMapped]
        [Display(Name = "Course Image")]
        [Required(ErrorMessage = "Please choose an image")]
        public IFormFile? FormFile { get; set; }
    }
}
