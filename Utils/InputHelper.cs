using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Utils
{
    public static class InputHelper
    {
        public static string ReadString(string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine() ?? string.Empty;

            while (string.IsNullOrWhiteSpace(input))
            {
                Console.Write("Invalid option. Try again: ");
                input = Console.ReadLine() ?? string.Empty;
            }

            return input;
        }

        public static int ReadInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
        {
            Console.Write(prompt);
            int value;

            while (!int.TryParse(Console.ReadLine(), out value) || value < min || value > max)
            {
                if (min == int.MinValue || max == int.MaxValue)
                    Console.Write($"Enter a valid integger: ");
                else
                    Console.Write($"Enter a valid integger between {min} and {max}: ");
            }

            return value;
        }

        public static decimal ReadDecimal(string prompt, decimal min = decimal.MinValue, decimal max = decimal.MaxValue)
        {
            Console.Write(prompt);
            decimal value;

            while (!decimal.TryParse(Console.ReadLine(), out value) || value < min || value > max)
            {
                if (min == decimal.MinValue || max == decimal.MaxValue)
                    Console.Write($"Enter a valid decimal number: ");
                else
                    Console.Write($"Enter a valid decimal number between {min} and {max}: ");
            }

            return value;
        }

        public static string ReadOption(string prompt, string[] validOptions)
        {
            Console.Write(prompt);
            string input = (Console.ReadLine() ?? string.Empty).Trim();

            while (Array.IndexOf(validOptions, input) == -1)
            {
                Console.Write($"Invalid option. Try again: ");
                input = (Console.ReadLine() ?? string.Empty).Trim();
            }

            return input;
        }
    }
}
