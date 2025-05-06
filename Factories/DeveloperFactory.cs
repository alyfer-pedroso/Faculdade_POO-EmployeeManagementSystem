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
            developer.OvertimeHours = InputHelper.ReadInt("Enter Overtime Hours: ", 0);
            developer.OvertimeRate = InputHelper.ReadDecimal("Enter Overtime Rate: ", 0);
            return developer;
        }
    }
}
