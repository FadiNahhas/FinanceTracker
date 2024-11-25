using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Transaction> Transactions { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Transaction>()
            .Property(t => t.Amount)
            .HasPrecision(18, 2);
        
        builder.Entity<Transaction>()
            .HasOne(t => t.User)
            .WithMany(u => u.Transactions)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}