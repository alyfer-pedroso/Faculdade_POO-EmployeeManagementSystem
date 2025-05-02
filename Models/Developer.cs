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
            Console.WriteLine($"Total Salary (${BaseSalary} + ({OvertimeHours} * ${OvertimeRate:F2})): {SalaryWithOvertime():F2}");
            Console.WriteLine($"Taxes: ${SalaryWithOvertime():F2} * ${(_taxes * 100):F2}% = {CalculateTaxes():F2}");
            Console.WriteLine($"Net Salary: ${CalculateSalary():F2}");
        }
    }
}
