using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Factories
{
    public class InternFactory: IEmployeeFactory
    {
        public Employee CreateEmployee() => new Intern();
    }
}
