using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace BitchAbout.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {

        }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public virtual ICollection<Rant> AllRants { get; set; }

    }
}
