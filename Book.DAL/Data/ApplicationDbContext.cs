using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookShop.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //// Set composite key for OrderDetail.
            builder.Entity<OrderDetail>()
                .HasKey(od => new { od.OrderId, od.BookId });

            //// Set composite key for BookGenre.
            builder.Entity<BookGenre>()
                .HasKey(bg => new { bg.GenreId, bg.BookId });

            //// Set composite key for BookAuthor.
            builder.Entity<BookAuthor>()
               .HasKey(ba => new { ba.AuthorId, ba.BookId });
        }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
    }
}
