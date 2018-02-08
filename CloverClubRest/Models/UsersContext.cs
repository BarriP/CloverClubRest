using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CloverClubRest.Models
{
    public partial class UsersContext : DbContext
    {
        public virtual DbSet<CoctelFav> CoctelesFav { get; set; }
        public virtual DbSet<IngredienteFav> IngredientesFav { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public UsersContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CoctelFav>(entity =>
            {
                entity.HasKey(e => new { e.Userid, e.Coctelid });

                entity.Property(e => e.Userid).HasColumnName("USERID");

                entity.Property(e => e.Coctelid).HasColumnName("COCTELID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CoctelesFav)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<IngredienteFav>(entity =>
            {
                entity.HasKey(e => new { e.Userid, e.Ingredienteid });

                entity.Property(e => e.Userid).HasColumnName("USERID");

                entity.Property(e => e.Ingredienteid).HasColumnName("INGREDIENTEID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.IngredientesFav)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME");

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasColumnName("PASS");
            });
        }
    }
}
