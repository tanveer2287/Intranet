using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using DataViewModels;
using OrgWebAPI.Repository.Entities;

namespace OrgWebAPI.AutoMapper
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeVM>();
            CreateMap<EmployeeVM, Employee>();
        }
    }
}
