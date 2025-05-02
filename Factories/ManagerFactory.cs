using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Factories
{
    public class ManagerFactory: IEmployeeFactory
    {
        public Employee CreateEmployee()
        {
            Manager manager = new();
            Console.Write("Enter Bonus: ");
            manager.Bonus = decimal.Parse(Console.ReadLine() ?? "0");
            return manager;
        }
    }
}
