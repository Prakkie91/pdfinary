using HtmlToPdf.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HtmlToPdf.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<SubscriptionType> SubscriptionTypes { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ApiRequest> ApiRequests { get; set; }



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
