using Core.Domain;
using Core.Domain.Interface;

namespace OrgWebAPI.Repository.Entities
{
    public class Employee :Entity, IAggregateRoot
    {

        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public double  Salary { get; set; }
    }
}
