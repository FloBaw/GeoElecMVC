using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthPostMVC.Models
{
    public class UserRoles
    {
        //[Required]
        //public string UserId { get; set; }

        //[Required]
        //public string RoleId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Name { get; set; }

    }
}
