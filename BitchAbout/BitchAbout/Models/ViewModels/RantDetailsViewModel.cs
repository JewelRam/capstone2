using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BitchAbout.Data;

namespace BitchAbout.Models.ViewModels
{
    public class RantDetailsViewModel
    {
        private ApplicationDbContext _context;

        public RantDetailsViewModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Rant Rant { get; set; }
    }
}
