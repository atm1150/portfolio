using Assessment.Models;
using Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.View
{
    //this purpose of this class is to take in user input and perform data validation
    public class UserIO
    {
        CommonDisplayMessages commonDisplay = new CommonDisplayMessages();


        //this method takens an integer input from the user and validates the data
        public int ReadInt(string prompt, int min, int max)
        {
            int output;

            while (true)
            {
                Console.WriteLine(prompt);
                string userInput = Console.ReadLine();
                if (int.TryParse(userInput, out output))
                {
                    if (output >= min && output <= max)
                    {
                        break; //valid input
                    }
                    else
                    {
                        //error given when the integer input is not in within the neccessary boundaries
                        string errorMessage = $"That number was not {min} through {max}. Please try again.";
                        commonDisplay.DisplayErrorMessage(errorMessage);
                    }
                }
                else
                {
                    //error message for not putting in an integer
                    commonDisplay.DisplayErrorMessage("That was not a valid input. Please try again.");
                }
            }
            return output;
            //throw new NotImplementedException();
        }

        //same as above but no max
        public int ReadInt(string prompt, int min)
        {
            int output;

            while (true)
            {
                Console.WriteLine(prompt);
                string userInput = Console.ReadLine();
                if (int.TryParse(userInput, out output))
                {
                    if (output >= min)
                    {
                        break; //valid input
                    }
                    else
                    {
                        //error given when the integer input is not in within the neccessary boundaries
                        string errorMessage = $"That number was not above {min}. Please try again.";
                        commonDisplay.DisplayErrorMessage(errorMessage);
                    }
                }
                else
                {
                    //error message for not putting in an integer
                    commonDisplay.DisplayErrorMessage("That was not a valid input. Please try again.");
                }
            }

            return output;
        }

        //this method takes in a string
        public string ReadString(string prompt)
        {
            string output;

            while (true)
            {
                Console.WriteLine(prompt);
                string userInput = Console.ReadLine();
                if (!userInput.Contains(','))
                {
                    output = userInput;
                    break;
                }
                else
                {
                    string errorMessage = "Commas cannot be used in data entry due to potential errors involving data storage. Please retype in the requested information without using a comma.";
                    commonDisplay.DisplayErrorMessage(errorMessage);
                    Console.Clear();
                }
            }
            return output;
            throw new NotImplementedException();
        }

        //takes in a decimal input and performs data validation
        public decimal ReadDecimal(string prompt, decimal min)
        {
            decimal output;

            while (true)
            {
                Console.WriteLine(prompt);
                string userInput = Console.ReadLine();
                if (decimal.TryParse(userInput, out output))
                {
                    if (output >= min)
                    {
                        break; //valid input
                    }
                    else
                    {
                        //error given when the integer input is not in within the neccessary boundaries
                        string errorMessage = $"That number was not above or equal to {min}. Please try again.";
                        commonDisplay.DisplayErrorMessage(errorMessage);
                        Console.Clear();
                    }
                }
                else
                {
                    //error message for not putting in an integer
                    commonDisplay.DisplayErrorMessage("That was not a valid input. Please try again.");
                    Console.Clear();
                }
            }

            return output;
            throw new NotImplementedException();
        }

        //method that returns a date along with a bool value. the bool value can then be used to end the display value early and return a user to the main menu if the wrong date is put in
        public GetDateResponse GetDateDisplay()
        {
            GetDateResponse response = new GetDateResponse();
            
                string prompt = "Please type in the date of the orders you wish to display.";
                commonDisplay.DisplayMessage(prompt);
                string month = ReadInt("Please type the month number", 1, 12).ToString();
                string day = ReadInt("Please type in the day number", 1, 31).ToString();
                string year = ReadInt("Please type in the year number", 0).ToString();
                //not sure if the forward slashes can be left out of tryparse so i'm throwing them in there
                string tempdate = month + "/" + day + "/" + year;
                DateTime orderDate;
                DateTime.TryParse(tempdate, out orderDate);

                if (DateTime.TryParse(tempdate, out orderDate))
                {
                    response.OrderDate = orderDate;
                    response.Success = true;
                    response.Message = "";

                    return response;
                }
                else
                {
                    //response.OrderDate = orderDate;
                    response.Success = false;
                    response.Message = "The date entered is not a real date. Returning to the main menu.";

                    return response;
                }
            
        }

        //gets date but loops if you do not put in a real date
        public DateTime GetDate()
        {
            while (true)
            {
                string prompt = "Please type in the date of the order you wish to add.";
                commonDisplay.DisplayMessage(prompt);
                string month = ReadInt("Please type the month number", 1, 12).ToString();
                string day = ReadInt("Please type in the day number", 1, 31).ToString();
                string year = ReadInt("Please type in the year number", 0).ToString();
                //not sure if the forward slashes can be left out of tryparse so i'm throwing them in there
                string tempdate = month + "/" + day + "/" + year;
                DateTime orderDate;
                DateTime.TryParse(tempdate, out orderDate);
                string date = orderDate.ToString("MMddyyyy");

                if (DateTime.TryParse(tempdate, out orderDate))
                {
                    return orderDate;
                }
                else
                {
                    string errorPrompt = "The date entered is not a real date.";
                    commonDisplay.DisplayErrorMessage(errorPrompt);
                    Console.Clear();
                }
            }
        }

        //takes in a list of the order data to display the information in a nice format
        public void DisplayAllOrders(List<Order> orderList)
        {
            Console.Clear();

            //Order order = orders[0];

            foreach (Order order in orderList)
            {
                //convert non string data types to strings for console.writeline
                string date = order.OrderDate.ToString();
                string orderNumber = order.OrderNumber.ToString();
                string materialCost = order.MaterialCost.ToString();
                string laborCost = order.LaborCost.ToString();
                string tax = order.Tax.ToString();
                string total = order.Total.ToString();

                //display order to console in a formatted form
                Console.WriteLine("**************************************");
                Console.WriteLine(orderNumber + " " + date);
                Console.WriteLine(order.CustomerName);
                Console.WriteLine(order.State);
                Console.WriteLine("Product: " + order.ProductType);
                Console.WriteLine("Materials: " + materialCost);
                Console.WriteLine("Labor: " + laborCost);
                Console.WriteLine("Tax: " + tax);
                Console.WriteLine("Total: " + total);
                Console.WriteLine("**************************************");

                //remove the comment tags below if you want each order to appear and require a key click before moving to the next one
              //  commonDisplay.HitAnyKeyToContinue();

            }
            commonDisplay.HitAnyKeyToContinue();
        }

        //display single order
        public void DisplayOrder(Order order)
        {
            //convert non string data types to strings for console.writeline
            string date = order.OrderDate.ToString();
            string orderNumber = order.OrderNumber.ToString();
            string materialCost = order.MaterialCost.ToString();
            string laborCost = order.LaborCost.ToString();
            string tax = order.Tax.ToString();
            string total = order.Total.ToString();

            //display order to console in a formatted form
            Console.WriteLine("**************************************");
            Console.WriteLine(orderNumber + " " + date);
            Console.WriteLine(order.CustomerName);
            Console.WriteLine(order.State);
            Console.WriteLine("Product: " + order.ProductType);
            Console.WriteLine("Materials: " + materialCost);
            Console.WriteLine("Labor: " + laborCost);
            Console.WriteLine("Tax: " + tax);
            Console.WriteLine("Total: " + total);
            Console.WriteLine("**************************************");
        }
    }
}
