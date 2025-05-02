using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class Intern: Employee
    {
        private readonly decimal _taxes = 0.10m;
        public Intern()
        {
            Role = "Intern";
        }

        public override decimal CalculateSalary() => BaseSalary;

        public override decimal CalculateTaxes() => 0.10m;
    }
}
