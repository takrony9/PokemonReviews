using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Entities;

namespace PokemonReviewApp.Data
{
    public class PokemonDbContext : DbContext
    {
        public PokemonDbContext(DbContextOptions<PokemonDbContext> options) : base(options) 
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<PokemonCategory> PokemonCategories { get; set; }
        public DbSet<PokemonOwner> PokemonOwners { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PokemonCategory>()
                .HasKey(bc => new { bc.PokemonId, bc.CategoryId });
            modelBuilder.Entity<PokemonCategory>()
                .HasOne(bc => bc.Pokemon)
                .WithMany(b => b.PokemonCategories)
                .HasForeignKey(bc => bc.PokemonId);
            modelBuilder.Entity<PokemonCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.PokemonCategories)
                .HasForeignKey(bc => bc.CategoryId);
            //////////////////////////////////////////////////////////
            modelBuilder.Entity<PokemonOwner>()
                .HasKey(bc => new { bc.PokemonId, bc.OwnerId });
            modelBuilder.Entity<PokemonOwner>()
                .HasOne(bc => bc.Pokemon)
                .WithMany(b => b.PokemonOwners)
                .HasForeignKey(bc => bc.PokemonId);
            modelBuilder.Entity<PokemonOwner>()
                .HasOne(bc => bc.Owner)
                .WithMany(c => c.PokemonOwners)
                .HasForeignKey(bc => bc.OwnerId);
        }

    }
}
