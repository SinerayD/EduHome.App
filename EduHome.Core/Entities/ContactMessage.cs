using EduHomeCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.Entities
{
    public class ContactMessage:BaseModel
    {
        public string Name { get; set; }    
        public string Email { get; set; }
        public string Subject { get; set; } 
        public string MessageArea { get; set; } 

    }
}
