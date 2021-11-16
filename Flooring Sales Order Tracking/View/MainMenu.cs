using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.View
{
    public class MainMenu
    {
        public void DisplayMainMenu()
        {
            Console.Clear();
            Console.Write("**************************************");
            Console.WriteLine("\nEnter a choice from the menu below:");
            Console.WriteLine("1.Display Orders");
            Console.WriteLine("2.Add an Order");
            Console.WriteLine("3.Edit an Order");
            Console.WriteLine("4.Remove an Order");
            Console.WriteLine("5.Exit Program");
            Console.WriteLine("**************************************");
        }
    }
}
