#region

using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Core.Domain.Interface;
using System.Threading.Tasks;
using OrgWebAPI.Repository.Entities;
using OrgWebAPI.Repository.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

#endregion

namespace OrgWebAPI.Repository
{
    public class APIContext : DbContext
    {
        private readonly IHttpContextAccessor _context;

        public APIContext(DbContextOptions<APIContext> options) : base(options)
        {

        }

        public APIContext(DbContextOptions<APIContext> options, IHttpContextAccessor context) :
            base(options)
        {
            _context = context;
            BeforeSaveEvent += UpdateAudit;
        }

        public event Action<DbContext> BeforeSaveEvent = _ => { };
        public virtual DbSet<Employee> Employees { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.AddEntityConfigurationsFromAssembly(GetType().GetTypeInfo().Assembly);

            // Shadow properties
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                .Where(e => typeof(IAuditable).IsAssignableFrom(e.ClrType)))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property<DateTime?>("CreatedDate");

                modelBuilder.Entity(entityType.ClrType)
                    .Property<DateTime?>("ModifiedDate");

                modelBuilder.Entity(entityType.ClrType)
                    .Property<string>("CreatedBy");

                modelBuilder.Entity(entityType.ClrType)
                    .Property<string>("ModifiedBy");
            }

            base.OnModelCreating(modelBuilder);
        }

        private void UpdateAudit(DbContext applicationDbContext)
        {
            var user = _context.HttpContext.User;
            var auditable = applicationDbContext.ChangeTracker.Entries<IAuditable>();

            var dbEntityEntries = auditable as EntityEntry<IAuditable>[] ?? auditable.ToArray();

            dbEntityEntries.Where(entry => entry.State == EntityState.Added)
                .ToList()
                .ForEach(a =>
                {
                    a.Property("CreatedDate").CurrentValue = DateTime.UtcNow;
                    a.Property("CreatedBy").CurrentValue = user.Identity.Name;
                });

            dbEntityEntries.Where(entry => entry.State == EntityState.Modified)
                .ToList()
                .ForEach(a =>
                {
                    a.Property("ModifiedDate").CurrentValue = DateTime.UtcNow;
                    a.Property("ModifiedBy").CurrentValue = user.Identity.Name;
                });
        }

        public override int SaveChanges()
        {
            BeforeSaveEvent(this);
            return base.SaveChanges();
        }

       
    }
}