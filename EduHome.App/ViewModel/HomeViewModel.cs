using EduHome.Core.Entities;
using EduHomeCore.Entities;

namespace EduHome.App.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<Teacher> Teachers { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<Notice> Notices { get; set; }
        public Setting Settings { get; set; }
    }
}
