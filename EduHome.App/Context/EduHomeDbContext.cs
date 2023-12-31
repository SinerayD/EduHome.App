﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EduHome.Core.Entities;
using EduHomeCore.Entities;
using EduHome.Core.Entities;

namespace EduHomeApp.Context
{
    public class EduHomeDbContext : IdentityDbContext<AppUser> 
    { 
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Social> Socials { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<CourseCategory> CourseCategories { get; set; }
        public DbSet<CourseAssets> CourseAssetss { get; set; }
        public DbSet<BlogTag> BlogTags { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<CourseTag> CourseTags { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseLanguage> CourseLanguages { get; set; }
        public DbSet<TeacherHobby> TeacherHobbies { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Notice> Notices { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }  
        public EduHomeDbContext(DbContextOptions<EduHomeDbContext> options) : base(options)
        {

        }

    }
}