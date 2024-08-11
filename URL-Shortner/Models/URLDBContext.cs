namespace URL_Shortner.Models;
using Microsoft.EntityFrameworkCore;
public class URLDBContext: DbContext
{
    public virtual DbSet<ShortURL> ShortURLs { get; set; }
    public URLDBContext(DbContextOptions<URLDBContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShortURL>().HasIndex(u => u.ShortenedURL).IsUnique();
    }
}