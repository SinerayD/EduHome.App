using EduHome.Core.Entities;
using EduHomeCore.Entities;

namespace EduHome.App.ViewModel
{
    public class HomeViewModel
    {
        public List<Teacher> Teachers { get; set; }
        public List<Course> Courses { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<Notice> Notices { get; set; }
        public Setting Settings { get; set; }
    }
}
