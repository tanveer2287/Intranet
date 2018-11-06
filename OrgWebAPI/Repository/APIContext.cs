#region

using System;
using System.Linq;
using System.Reflection;
using Core.Domain;
using Core.Domain.Interface;
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
        }
    

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

    public override int SaveChanges()
    {
        
        return base.SaveChanges();
    }
}
}