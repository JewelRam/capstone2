using BitchAbout.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitchAbout.Models.ViewModels
{
    public class RantProfRantListViewModel
    {
        public Rant Rant { get; set; }

        public IEnumerable<Rant> Prof_Rants { get; set; }


        public RantProfRantListViewModel(ApplicationDbContext context)
        {
            Prof_Rants = context.Rant.Where(p => p.Review != null);
            //Prof_Rants = context.Rant;


        }
    }
    
}

