using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataViewModels;
namespace Services.Interrface
{
    public interface IEmployeeService
    {
        Task<List<EmployeeVM>> GetEmployees();
        Task<bool> SaveEmployee(EmployeeVM model);

        Task<EmployeeVM> GetEmployee(string id);

        Task<bool> UpdateEmployee(string id, EmployeeVM model);

        Task<bool> DeleteEmployee(string id);
    }
}
