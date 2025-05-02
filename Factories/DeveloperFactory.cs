using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Utils;

namespace EmployeeManagementSystem.Factories
{
    public class DeveloperFactory: IEmployeeFactory
    {
        public Employee CreateEmployee()
        {
            Developer developer = new();
            developer.OvertimeHours = InputHelper.ReadInt("Enter Overtime Hours: ");
            developer.OvertimeRate = InputHelper.ReadDecimal("Enter Overtime Rate: ");
            return developer;
        }
    }
}
