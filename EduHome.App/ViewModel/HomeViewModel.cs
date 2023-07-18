using EduHome.Core.Entities;

namespace EduHome.App.ViewModel
{
    public class HomeViewModel
    {
        public About About{ get;set;}
        public IEnumerable<Teacher> Teachers { get; set; }
    }
}
