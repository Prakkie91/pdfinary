using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pdfinary.Models;

namespace Pdfinary.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<Render> Renders { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
