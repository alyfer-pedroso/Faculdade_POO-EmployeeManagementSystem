using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Factories
{
    public class DeveloperFactory: IEmployeeFactory
    {
        public Employee CreateEmployee()
        {
            Developer developer = new();
            Console.Write("Enter Overtime Hours: ");
            developer.OvertimeHours = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Enter Overtime Rate: ");
            developer.OvertimeRate = decimal.Parse(Console.ReadLine() ?? "0");
            return developer;
        }
    }
}
