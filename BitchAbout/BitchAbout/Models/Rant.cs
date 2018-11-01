using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BitchAbout.Models
{
    public class Rant
    {
        [Key]
        public int RantId { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        
        public string Summary { get; set; }

        public string Review { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

    }
}
