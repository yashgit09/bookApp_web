using BookTrackingApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookTrackingApplication.Data
{
    public class BookTrackingApplicationContext : DbContext
    {
        public BookTrackingApplicationContext(DbContextOptions<BookTrackingApplicationContext> bookTrackingApplicationContext)
            : base(bookTrackingApplicationContext)
        { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryType> CategoryTypies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<CategoryType>().ToTable("CategoryType");
            
                   
                    

        }
    }
}
