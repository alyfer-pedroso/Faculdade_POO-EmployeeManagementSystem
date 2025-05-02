using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public abstract class Employee
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Role { get; protected set; } = string.Empty;
        public decimal BaseSalary { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string DeliveryMethod { get; set; } = string.Empty;

        public abstract decimal CalculateSalary();

        public virtual decimal CalculateTaxes() => 0m;

        public virtual void DeliverPayment() =>
                Console.WriteLine($"{Name}'s paymentt will be delivered via {DeliveryMethod} using {PaymentMethod}");

       public virtual void DisplayInfo()
        {
            Console.WriteLine($"\nName: {Name}");
            Console.WriteLine($"Age: {Age}");
            Console.WriteLine($"Role: {Role}");
            Console.WriteLine($"Base Salary: ${BaseSalary}");
            Console.WriteLine($"Payment Method: {PaymentMethod}");
            Console.WriteLine($"Delivery Method: {DeliveryMethod}");
        }
    }
}
