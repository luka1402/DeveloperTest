using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeveloperTest.Core.Domain.Abstraction;

namespace DeveloperTest.Core.Domain.Models
{
    public class Company : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<EmployeeCompany>? EmployeeCompanies { get; set; }
    }
}
