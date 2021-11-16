using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;


namespace Console.App
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuController menuController = new MenuController();

            menuController.Run();
        }
    }
}
