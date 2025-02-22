using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure one-to-one relationship
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithOne(a => a.Book)
                .HasForeignKey<Author>(a => a.BookId);

            modelBuilder.Entity<Author>()
                .HasOne(a => a.Book)
                .WithOne(b => b.Author)
                .HasForeignKey<Book>(b => b.AuthorId);
        }
    }
}

//EF Core

//Code first approach
