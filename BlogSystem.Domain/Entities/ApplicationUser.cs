using BlogSystem.Domain.Entities.Content;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Domain.Entities
{
    public class ApplicationUser:IdentityUser
    {
        public ICollection<Post>? Posts { get; set; } 
    }
}
