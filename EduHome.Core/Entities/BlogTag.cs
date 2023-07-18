using System.ComponentModel.DataAnnotations;
using EduHomeCore.Entities;

namespace EduHome.Core.Entities
{
    public class BlogTag:BaseModel
    {
        [Display(Name = "Blog")]
        public int BlogId { get; set; }

        public Blog? Blog { get; set; }

        [Display(Name = "Tag")]
        public int TagId { get; set; }

        public Tag? Tag { get; set; }
    }

}
