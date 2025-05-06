using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Utils;

namespace EmployeeManagementSystem.Factories
{
    public class ManagerFactory: IEmployeeFactory
    {
        public Employee CreateEmployee()
        {
            Manager manager = new();
            manager.Bonus = InputHelper.ReadDecimal("Enter Bonus: ", 0);
            return manager;
        }
    }
}
