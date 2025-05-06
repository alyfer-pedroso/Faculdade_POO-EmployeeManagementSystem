using System.Globalization;
using EmployeeManagementSystem.Menu;

namespace EmployeeManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

            ConsoleMenu menu = new ConsoleMenu();
            menu.Show();
        }
    }
}
