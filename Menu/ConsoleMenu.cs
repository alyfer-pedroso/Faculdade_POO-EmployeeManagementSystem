using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagementSystem.Factories;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Utils;

namespace EmployeeManagementSystem.Menu
{
    public class ConsoleMenu
    {
        private bool isRunning = false;

        private readonly List<Employee> employees = [];

        private static readonly string[] validRoles = new[] {"Manager", "Developer", "Intern"};
        private static readonly string[] validPaymentMethods = new[] { "Pix", "Bank Debit", "Cash" };
        private static readonly string[] validDeliveryMethods = new[] { "Automatic", "Manual" };

        private static readonly Dictionary<string, Func<IEmployeeFactory>> roleFactories = new()
        {
            [validRoles[0]] = () => new ManagerFactory(),
            [validRoles[1]] = () => new DeveloperFactory(),
            [validRoles[2]] = () => new InternFactory(),
        };

        public void Show()
        {
            Init();

            while (isRunning)
            {
                PrintOptions();
                UserChoice();
            }
        }

        private void AddEmployee()
        {
            Console.Clear();
            Console.WriteLine("\n--- Employee Management System ---\n");
            Console.Write("Adding Employee\n\n");

            string roleChoice = InputHelper.ReadOption($"Enter Role: ({string.Join(", ", validRoles)}):  ", validRoles);
            IEmployeeFactory factory = roleFactories.First(x => x.Key == roleChoice).Value();

            var employee = factory.CreateEmployee();

            employee.Name = InputHelper.ReadName("Enter Name: ");

            while (Array.IndexOf(employees.Select(x => x.Name).ToArray(), employee.Name) != -1)
            {
                Console.WriteLine("\nEmployee with this name already exists. Please enter a different name.\n");
                employee.Name = InputHelper.ReadString("Enter Name: ");
            }

            employee.Age = InputHelper.ReadInt("Enter Age: ", 15, 125);
            employee.BaseSalary = InputHelper.ReadDecimal("Enter Base Salary: ", 100);
            employee.PaymentMethod = InputHelper.ReadOption($"Enter Payment Method ({string.Join(", ", validPaymentMethods)}): ", validPaymentMethods);
            employee.DeliveryMethod = InputHelper.ReadOption($"Enter Delivery Method ({string.Join(", ", validDeliveryMethods)}): ", validDeliveryMethods);

            employees.Add(employee);
            Console.WriteLine("\nEmployee added successfully.\n");

            RequestInput();
        }

        private void ListEmployees()
        {
            Console.Clear();
            Console.WriteLine("\n--- Employee Management System ---\n");
            Console.WriteLine("List of Employees:\n");

            if (employees.Count == 0)
            {
                Console.WriteLine("No employees registered.");
                RequestInput();
                return;
            }

            foreach (var emp in employees)
                Console.WriteLine(emp.Name);

            string choice = InputHelper.ReadOption("\nView employee in detail? (yes, no): ", ["yes", "no"]);

            if (choice == "no")
            {
                RequestInput();
                return;
            }

            List<string> employeeNames = [.. employees.Select(x => x.Name)];
            string employeeSelected = InputHelper.ReadOption("\nSelect Employee: ", employeeNames.ToArray());

            SelectEmployee(employees.First(x => x.Name == employeeSelected));
        }

        private void SelectEmployee(Employee emp)
        {
            Console.Clear();
            Console.WriteLine("\n--- Employee Management System ---\n");
            Console.WriteLine("Selected Employee:");

            emp.DisplayInfo();

            string choice = InputHelper.ReadOption("\nDeliver payment (yes, no): ", ["yes", "no"]);

            if (choice == "yes")
                emp.DeliverPayment();

            RequestInput();
        }

        private void PrintOptions()
        {
            Console.WriteLine("\n--- Employee Management System ---\n");
            Console.WriteLine("1. Add Employee");
            Console.WriteLine("2. List Employees");
            Console.WriteLine("3. Exit");
            Console.Write("\nChoose an option: ");
        }

        private void UserChoice()
        {
            string choice = Console.ReadLine() ?? string.Empty;
            switch (choice)
            {
                case "1":
                    AddEmployee();
                    break;
                case "2":
                    ListEmployees();
                    break;
                case "3":
                    Stop();
                    break;
                default:
                    Console.Write("\nInvalid input. Press any key to continue.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
            }

        }

        private void RequestInput()
        {
            Console.Write("\nPress any key to continue.");
            Console.ReadKey();
            Console.Clear();
        }

        private void Init() => isRunning = true;

        private void Stop()
        {
            isRunning = false;

            Console.Clear();
            Console.WriteLine("\n--- Employee Management System ---\n");
            Console.WriteLine("Goodbye!");
        }
    }
}
