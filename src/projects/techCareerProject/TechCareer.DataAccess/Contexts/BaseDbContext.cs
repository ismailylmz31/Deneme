using System.Reflection;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TechCareer.Models.Entities;
using Microsoft.Extensions.Logging;

namespace TechCareer.DataAccess.Contexts;

public class BaseDbContext : DbContext
{
    public IConfiguration Configuration { get; set; }

    public BaseDbContext(DbContextOptions<BaseDbContext> opt, IConfiguration configuration) : base(opt)
    {
        Configuration = configuration;

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<VideoEducation> VideoEducations { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
}