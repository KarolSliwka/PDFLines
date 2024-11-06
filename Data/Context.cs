using Microsoft.EntityFrameworkCore;
using PDFLines.Models;

namespace PDFLines.Data
{
    public class TCZNT5000 : DbContext
    {
        public TCZNT5000(DbContextOptions<TCZNT5000> options) : base(options)
        { }

        public DbSet<User>? Users { get; set; }
        public DbSet<Client>? Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<UnitState>().ToTable(nameof(UnitStates), t => t.ExcludeFromMigrations());
            //modelBuilder.Entity<UnitStateHistory>().ToTable(nameof(UnitStatesHistory), t => t.ExcludeFromMigrations());
            //modelBuilder.Entity<User>().ToTable(nameof(Users), t => t.ExcludeFromMigrations());
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");
        }

        public DbSet<PDFLines.Models.Project>? Projects { get; set; }

        public DbSet<PDFLines.Models.Maintenance>? Maintenance { get; set; }
    }

    public class TCZNT58 : DbContext
    {
        public TCZNT58(DbContextOptions<TCZNT58> options) : base(options)
        { }

        public DbSet<Employee>? Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable(nameof(Employee), t => t.ExcludeFromMigrations());
        }
    }

}