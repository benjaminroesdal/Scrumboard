using DbComponent.DbModels;
using Microsoft.EntityFrameworkCore;

namespace ScrumboardApi.Context
{
    public class ScrumBoardContext : DbContext
    {
        public ScrumBoardContext(DbContextOptions<ScrumBoardContext> options) : base(options) 
        { 
        }

        public DbSet<State> States { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoardTask>()
                .HasOne(x => x.Assignee)
                .WithMany(x => x.AssigneeTasks)
                .HasForeignKey(x => x.AssigneeID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<BoardTask>()
                .HasOne(x => x.Reporter)
                .WithMany(x => x.ReporterTasks)
                .HasForeignKey(x => x.ReporterID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<BoardTask>().ToTable("BoardTask");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<State>().ToTable("State");
        }
    }
}
