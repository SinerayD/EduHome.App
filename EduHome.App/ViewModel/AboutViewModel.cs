﻿using EduHome.Core.Entities;

namespace EduHome.App.ViewModels
{
    public class AboutViewModel
    {
        public IEnumerable<Notice> Notices { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
        public Setting Setting { get; set; }
    }
}