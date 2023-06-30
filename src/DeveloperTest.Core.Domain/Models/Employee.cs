using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeveloperTest.Core.Domain.Abstraction;
using DeveloperTest.Core.Domain.EnumList;

namespace DeveloperTest.Core.Domain.Models
{
    public class Employee : BaseEntity
    {
        public int Id { get; set; }
        public TitleTypes Title { get; set; }
        public string Email { get; set; }

        public ICollection<EmployeeCompany>? EmployeeCompanies { get; set; }

    }
}
