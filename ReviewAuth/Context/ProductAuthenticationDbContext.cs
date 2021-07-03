using Microsoft.EntityFrameworkCore;

namespace ProductReviewAuthentication.Models
{
    public class ProductAuthenticationDbContext : DbContext
    {
        public ProductAuthenticationDbContext(DbContextOptions<ProductAuthenticationDbContext> options)
             : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Product)
                .WithOne(u => u.User);

            modelBuilder.Entity<Review>()
               .HasOne(r => r.Product)
               .WithMany(p => p.Review)
               .HasForeignKey(r => r.ProductId);

            modelBuilder.Entity<Review>()
               .HasOne(r => r.User)
               .WithMany(u => u.Review)
               .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Product>()
               .HasOne(p => p.User)
               .WithMany(u => u.Product)
               .HasForeignKey(r => r.UserId);



            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>().Property(u => u.Id).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Name).IsRequired().HasMaxLength(70);
            modelBuilder.Entity<User>().Property(u => u.CreatedAt).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.HashSalt).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Gender).IsRequired().HasMaxLength(6);

            modelBuilder.Entity<Product>().Property(p => p.Id).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(25);
            modelBuilder.Entity<Product>().Property(p => p.CreatedAt).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Description).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<Review>().Property(r => r.Id).IsRequired();
            modelBuilder.Entity<Review>().Property(r => r.Comment).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Review>().Property(r => r.Ratings).IsRequired();

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Review> Reviews { get; set; }
    }
}
