﻿using EduHomeCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EduHome.Core.Entities
{
    public class CourseCategory:BaseModel
    {
        [Display(Name = "Category")]
        public int CategoryId { get; set; } 
        public Category? Category { get; set; }

        [Display(Name = "Course")]
        public int CourseId { get; set; }
        public Course? Course { get; set; }
    }
}
