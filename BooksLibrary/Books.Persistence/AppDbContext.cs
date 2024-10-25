using Books.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Persistence
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("Authors");
                entity.HasKey(a => a.Id);
                entity.Property(a=> a.FirstName).IsRequired();
                entity.Property(a=> a.LastName).IsRequired();
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Books");
                entity.HasKey(b => b.Id);
                entity.Property(b => b.ISBN).IsRequired();
                entity.Property(b=>b.Title).IsRequired();

                //One to Many
                entity.HasOne(b=>b.Author)
                .WithMany(a=>a.Books)
                .HasForeignKey(b=>b.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
