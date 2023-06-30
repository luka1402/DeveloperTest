using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeveloperTest.Core.Application.Helper;
using DeveloperTest.Core.Application.Interfaces;
using DeveloperTest.Core.Domain.Abstraction;
using DeveloperTest.Core.Domain.Models;
using DeveloperTest.Core.Framework;
using Microsoft.EntityFrameworkCore;

namespace DeveloperTest.Infrastructure.Persistence
{
    public class DeveloperTestContext : DbContext, IDeveloperTestContext, IUnitOfWork
    {
        public DeveloperTestContext() : base() { }

        public DeveloperTestContext(DbContextOptions<DeveloperTestContext>  options) : base(options) { }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added
                    ));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.Now.SetKindUtc();
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<EmployeeCompany> EmployeeCompanies { get; set; }
        public DbSet<SystemLog> SystemLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SystemLog>(entity =>
            {
                entity.Property(e => e.Changeset)
                    .HasColumnType("jsonb");
            });
        }
    }
}
