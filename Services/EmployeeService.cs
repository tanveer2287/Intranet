using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataViewModels;
using Services.Interrface;
using Core.Helpers;
using Newtonsoft.Json;
using Services.Helpers;

namespace Services
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        //private readonly IApiHttpClient _httpClient;
        public EmployeeService(ApiHttpClient httpClient) : base(httpClient)
        {

        }
        public async Task<List<EmployeeVM>> GetEmployees()
        {
            List<EmployeeVM> vm = new List<EmployeeVM>();
            var url = "api/Employees";
            var result = await apiClient.GetOrgClient().GetAsync(url);
            if (result.IsSuccessStatusCode)
            {
                var data = await result.Content.ReadAsStringAsync();
                vm = JsonConvert.DeserializeObject<List<EmployeeVM>>(data);
            }
            return vm;
        }

        public async Task<bool> SaveEmployee(EmployeeVM model)
        {
            List<EmployeeVM> vm = new List<EmployeeVM>();
            var url = "api/Employees";
            var result = await apiClient.GetOrgClient().PostAsync(url, new JsonContent(model));
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<EmployeeVM> GetEmployee(string id)
        {
            EmployeeVM vm = new EmployeeVM();
            var url = "api/Employees/" + id;
            var result = await apiClient.GetOrgClient().GetAsync(url);
            if (result.IsSuccessStatusCode)
            {
                var data = await result.Content.ReadAsStringAsync();
                vm = JsonConvert.DeserializeObject<EmployeeVM>(data);
            }

           return vm;
        }

        public async Task<bool> UpdateEmployee(string id, EmployeeVM model)
        {
            List<EmployeeVM> vm = new List<EmployeeVM>();
            var url = "api/Employees/" + id;
            var result = await apiClient.GetOrgClient().PutAsync(url, new JsonContent(model));
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }


        public async Task<bool> DeleteEmployee(string id)
        {
            List<EmployeeVM> vm = new List<EmployeeVM>();
            var url = "api/Employees/" + id;
            var result = await apiClient.GetOrgClient().DeleteAsync(url);
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

    }
}
