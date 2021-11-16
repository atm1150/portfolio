using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.View
{
    public class CommonDisplayMessages
    {
        //this method will display all error messages by taking in the error message as a string and displaying the message in red. After displaying it will reset the color back to normal
        public void DisplayErrorMessage(string errorMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(errorMessage);

            Console.ResetColor();

            HitAnyKeyToContinue();
        }

        //method to to pause the app at appropriate times so the user has a chance to read program output and continue one when they wish
        public void HitAnyKeyToContinue()
        {
            Console.WriteLine("Please hit any key to continue.");

            Console.ReadKey();
        }

        //If a message needs to be displayed
        public void DisplayMessage(string prompt)
        {
            Console.WriteLine(prompt);
        }

        public void ClearScreen()
        {
            Console.Clear();
        }
    }
}
