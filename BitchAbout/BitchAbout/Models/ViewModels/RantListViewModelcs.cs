using BitchAbout.Data;
using BitchAbout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitchAbout.Models.ViewModels
{
    public class RantListViewModel
    {
        public Rant Rant { get; set; }
        public IEnumerable<Rant> Rants { get; set; }

        public RantListViewModel(ApplicationDbContext context, ApplicationUser User)
        {
            Rants = context.Rant.Where(u => u.ApplicationUserId == User.Id);


        }
    }
    
}
