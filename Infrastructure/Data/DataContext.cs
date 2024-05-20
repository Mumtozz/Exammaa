using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<ClassSchedule> ClassSchedules { get; set; }
    public DbSet<Membership> Memberships { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<WorkOut> WorkOuts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}