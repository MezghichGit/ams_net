using Microsoft.EntityFrameworkCore;
using ams.Dtos;

namespace ams.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Provider> Providers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .HasOne(a => a.Provider)
                .WithMany(p => p.Articles)
                .HasForeignKey(a => a.ProviderId);
        }
        public DbSet<ams.Dtos.ProviderDto> ProviderDto { get; set; }
        public DbSet<ams.Dtos.ArticleDto> ArticleDto { get; set; }
    }
}
