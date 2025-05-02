using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class Manager: Employee
    {
        public decimal Bonus { get; set; }

        const decimal _taxes = 0.275m;

        public Manager()
        {
            Role = "Manager";
        }
        public decimal SalaryWithBonus() => BaseSalary + Bonus;

        public override decimal CalculateTaxes() => SalaryWithBonus() * _taxes;

        public override decimal CalculateSalary()
        {
            decimal gross = SalaryWithBonus();
            return gross - CalculateTaxes();
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Bonus: ${Bonus}");
            Console.WriteLine($"Total Salary: ${SalaryWithBonus():F2}");
            Console.WriteLine($"Taxes: ${SalaryWithBonus():F2} * ${(_taxes * 100):F2}% = {CalculateTaxes():F2}");
            Console.WriteLine($"Net Salary: ${CalculateSalary():F2}");
        }
    }
}
