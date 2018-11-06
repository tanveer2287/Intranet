using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Repository;
using Microsoft.AspNetCore.Mvc;
using DataViewModels;
using OrgWebAPI.Repository.Entities;
using System.Linq;
using AutoMapper;

namespace OrgWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IReader<Employee> _empReader;
        private readonly IWriter<Employee> _empWriter;
        private readonly IMapper _mapper;

        public EmployeesController(IReader<Employee> empReader, IWriter<Employee> empWriter, IMapper mapper)
        {
            _empReader = empReader;
            _empWriter = empWriter;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<EmployeeVM> employees = new List<EmployeeVM>();
            var employeeList =   await _empReader.All().OrderBy(x=>x.EmployeeId).ToAsyncEnumerable().ToList();

             employees = _mapper.Map<List<Employee>, List<EmployeeVM>>(employeeList);

            return Ok(employees);
        }

        // GET: api/Employees/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get([FromRoute]string id)
        {
            EmployeeVM employeeVM = new EmployeeVM();
            var employee = await _empReader.FindByAsync(x => x.EmployeeId == id);
            employeeVM = _mapper.Map<Employee, EmployeeVM>(employee);
            return Ok(employeeVM);
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeVM model)
        {
            var employeeVM = _mapper.Map<EmployeeVM, Employee>(model);
            await _empWriter.AddAsync(employeeVM);
            return Ok();
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute]string id, [FromBody] EmployeeVM model)
        {
            var employee = await _empReader.FindByAsync(x => x.EmployeeId == id);
            if (employee != null)
            {
                var employeeVM = _mapper.Map<EmployeeVM, Employee>(model);
                await _empWriter.UpdateAsync(employeeVM);
            }
          
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]string id)
        {
            var employee = await _empReader.FindByAsync(x => x.EmployeeId == id);
            if (employee != null)
            {
                _empWriter.Delete(employee);
            }
            return Ok();
        }
    }
}
