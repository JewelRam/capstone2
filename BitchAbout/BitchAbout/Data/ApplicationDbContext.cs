using System;
using System.Collections.Generic;
using System.Text;
using BitchAbout.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BitchAbout.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
                    public DbSet<ApplicationUser> ApplicationUser { get; set; }
                    public DbSet<Rant> Rant { get; set; }


    }
}
