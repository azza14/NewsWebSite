using Microsoft.EntityFrameworkCore;

namespace NewsWebSite.Models
{
    public class NewsDbContext : DbContext
    {

        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options)
        { }
        public DbSet<TemMember> TemMembers { get; set; }
        public DbSet<New> News { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ContactUs> Contacts { get; set; }
    }
}
