using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrgWebAPI.Repository.Entities;
using OrgWebAPI.Repository.Helpers;
namespace OrgWebAPI.Repository.Mapping
{
    public class EmployeeMap :EntityMappingConfiguration<Employee>
    {
        public override void Map(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(x => x.FirstName).IsRequired(true);
        }
    }
}
