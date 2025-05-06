using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class Developer: Employee
    {
        public int OvertimeHours { get; set; }
        public decimal OvertimeRate { get; set; }


        const decimal _taxes = 0.10m;

        public Developer()
        {
            Role = "Developer";
        }

        public decimal SalaryWithOvertime() => BaseSalary + (OvertimeHours * OvertimeRate);

        public override decimal CalculateTaxes() => SalaryWithOvertime() * _taxes;

        public override decimal CalculateSalary()
        {
            decimal gross = SalaryWithOvertime();
            return gross - CalculateTaxes();
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Overtime Hours: {OvertimeHours}");
            Console.WriteLine($"Overtime Rate: {OvertimeRate}");
            Console.WriteLine($"Total Salary (${BaseSalary:0.00} + ({OvertimeHours} * ${OvertimeRate:0.00})): {SalaryWithOvertime():0.00}");
            Console.WriteLine($"Taxes: ${SalaryWithOvertime():0.00} * ${(_taxes * 100):0.00}% = {CalculateTaxes():0.00}");
            Console.WriteLine($"Net Salary: ${CalculateSalary():0.00}");
        }
    }
}
