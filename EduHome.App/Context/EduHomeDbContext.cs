﻿
using EduHome.Core.Entities;
using EduHomeCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduHomeApp.Context
{
    public class EduHomeDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<About> Abouts { get; set; }
        public EduHomeDbContext(DbContextOptions<EduHomeDbContext> options) : base(options)
        {

        }

    }
}