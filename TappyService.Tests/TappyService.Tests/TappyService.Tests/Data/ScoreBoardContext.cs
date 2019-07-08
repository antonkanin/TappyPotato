using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;

namespace Ntl.TappyService.Tests.Data
{
    public class ScoreBoardContext : DbContext
    {
        public DbSet<ScoreBoardItem> ScoreBoard { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=tappypotato;user=tappypotato;password=tappypotato");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ScoreBoardItem>(entity =>
            {
                entity.ToTable("score_board");
                entity.HasKey(e => e.Number);
                entity.Property(e => e.Number).ValueGeneratedOnAdd();
                entity.Property(e => e.PlayerId).IsRequired();
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Score).IsRequired();
                entity.Property(e => e.Position).IsRequired();
                entity.Property(e => e.Created).IsRequired();
                entity.Property(e => e.Version).IsRequired();
            });

        }
    }
}