using Assessment.Models;
using Assessment.View;
using BLL;
using BLL.Factories;
using BLL.Managers;
using BLL.RepoManagers;
using Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class MenuController
    {
        OrderRepoManager orderRepoManager = OrderManagerFactory.Create();
        UserIO userIO = new UserIO();
        MainMenu mainMenu = new MainMenu();
        CommonDisplayMessages commonDisplay = new CommonDisplayMessages();
        ProductRepoManager productRepoManager = ProductManagerFactory.Create();
        TaxRepoManager taxRepoManager = TaxesManagerFactory.Create();
        

        //Main menu controller
        public void Run()
        {
            bool keepRunning = true;

            while (keepRunning)
            {
                mainMenu.DisplayMainMenu();

                int menuChoice = userIO.ReadInt("Please type in the number that corresponds with the menu option you need.", 1, 5);

                switch (menuChoice)
                {
                    case 1:
                        DisplayOrders();
                        break;
                    case 2:
                        AddOrder();
                        break;
                    case 3:
                        EditOrder();
                        break;
                    case 4:
                        DeleteOrder();
                        break;
                    case 5:
                        keepRunning = false;
                        break;
                }
            }
        }
        private void DisplayOrders()
        {   
            GetDateResponse dateResponse = new GetDateResponse();

            //get date response of requested orders
            dateResponse = userIO.GetDateDisplay();

            //get list of orders
            ListOrderResponse orderResponse = orderRepoManager.LoadOrderList(dateResponse.OrderDate);

            if (dateResponse.Success && orderResponse.Success)
            {
                //send the order list from the response to the view for display
                userIO.DisplayAllOrders(orderResponse.Orders);
            }
            else if(dateResponse.Success == false)
            {
                commonDisplay.DisplayErrorMessage(dateResponse.Message);
            }
            else if (!orderResponse.Success)
            {
                commonDisplay.DisplayErrorMessage(orderResponse.Message);
            }
        }

        private void AddOrder()
        {
            Order orderObject = new Order();
            OrderResponse orderProdResponse = new OrderResponse();
            OrderResponse orderTaxResponse = new OrderResponse();

            Random random = new Random();

            //get the order date. must be in the future
            DateTime orderDate = userIO.GetDate();
            if (!(orderDate > DateTime.Now))
            {
                commonDisplay.DisplayErrorMessage("New orders must have a date in the future");
                return;
            }

            //get customer name
            string customerName = userIO.ReadString("Please type in the name of the company or person whose order you are entering in");
            //get the square foot amount of product the customer wants installed
            decimal area = userIO.ReadDecimal("Please type in the sqaure foot amount of product you wish installed. Number must be greater than 100", 100);

            //get the name of the product they wish to have installed
            while (true)
            {
                string productName = userIO.ReadString("Please type in the name of the product the customer wants installed").ToLower();
                //validate if user product input matches any products in database and return said product data if there is a match. loops if no match found
                OrderResponse prodCheck = productRepoManager.AddProductDataToOrderObject(productName);
                if (prodCheck.Success != false)
                {
                    orderProdResponse = prodCheck;
                    break;
                }
                commonDisplay.DisplayErrorMessage(prodCheck.Message);           
            }

            //get the state they are in for Order object and tax lookup purposes
            while (true)
            {
                string stateName = userIO.ReadString("Please type in the state where the customer is located").ToLower();
                //validate that input state exists in database and return object data. loops if no match is found
                OrderResponse taxCheck = taxRepoManager.AddTaxDataToOrderObject(stateName);
                if (taxCheck.Success != false)
                {
                    orderTaxResponse = taxCheck;
                    break;
                }
                commonDisplay.DisplayErrorMessage(taxCheck.Message);
            }

            //set all other data to order object
            orderObject = SetInputsToOrderObject(customerName, area, orderDate, orderProdResponse, orderTaxResponse);
            //to get the order number it will load up the selected date and check how many orders currently exist and increment the number by 1. if none exists then the order number is set to one
            orderObject.OrderNumber = SetOrderNumber(orderDate);

            //display order summary and confirm the user wishes to save
            commonDisplay.ClearScreen();
            userIO.DisplayOrder(orderObject);
            string confirmation = userIO.ReadString("Do you wish to go ahead and save the above order? Type yes to save or anything to cancel").ToLower();

            if (confirmation == "yes")
            {
                //save the order
                orderRepoManager.SaveOrder(orderObject, orderDate);
            }  
        }

        private void EditOrder()
        {
            Order orderObject = new Order();
            OrderResponse orderProdResponse = new OrderResponse();
            OrderResponse orderTaxResponse = new OrderResponse();

            //get order date and number to find the order to be deleted
            DateTime orderDate = userIO.GetDate();
            int orderNumber = userIO.ReadInt("Please put in the order number of the order you wish to edit.", 1, 100);

            //load order and end method early if order not found
            OrderResponse editOrder = orderRepoManager.LoadOrder(orderDate, orderNumber);
            if (editOrder.Success == false)
            {
                commonDisplay.DisplayErrorMessage(editOrder.Message);
                return;
            }

            //display order that is to be edited
            commonDisplay.ClearScreen();
            userIO.DisplayOrder(editOrder.Order);

            //change order data
            //get customer name
            commonDisplay.DisplayMessage("Update company name");
            string customerName = GetEditInput();
            //blank entries are to stay the same as the old order
            if (customerName == "")
            {
                customerName = editOrder.Order.CustomerName;
            }

            //get the square foot amount of product the customer wants installed
            decimal area;
            decimal output;
            commonDisplay.DisplayMessage("Update the order area that product will cover");
            string stringArea = GetEditInput();
            if (stringArea == "")
            {
                area = editOrder.Order.Area;
            }
            else if (decimal.TryParse(stringArea, out output) && output >= 100)
            {
                area = output;
            }
            else
            {
                commonDisplay.DisplayErrorMessage("Something was entered that was either not a number or it was a number less than 100. Please input a correct area");
                area = userIO.ReadDecimal("Please type in the sqaure foot amount of product you wish installed. Number must be greater than 100", 100);
            }

            //get the name of the product they wish to have installed
            while (true)
            {
                commonDisplay.DisplayMessage("Update the name of the product you wish installed");
                string productName = GetEditInput().ToLower();
                if (productName == "")
                {
                    orderProdResponse.Order = editOrder.Order;
                    break;
                }
                //validate if user product input matches any products in database and return said product data if there is a match. end method early if no match is made
                OrderResponse prodCheck = productRepoManager.AddProductDataToOrderObject(productName);
                if (prodCheck.Success != false)
                {
                    orderProdResponse = prodCheck;
                    break;
                }
                commonDisplay.DisplayErrorMessage(prodCheck.Message);
            }

            //get the state they are in for Order object and tax lookup purposes
            while (true)
            {
                commonDisplay.DisplayMessage("Update the state in which the company is located");
                string stateName = GetEditInput().ToLower();
                if (stateName == "")
                {
                    orderTaxResponse.Order = editOrder.Order;
                    break;
                }

                //validate that input state exists in database and return object data. end method early if no match exists
                OrderResponse taxCheck = taxRepoManager.AddTaxDataToOrderObject(stateName);
                if (taxCheck.Success != false)
                {
                    orderTaxResponse = taxCheck;
                    break;
                }
                commonDisplay.DisplayErrorMessage(taxCheck.Message);
            }

            //set all other data to order object
            orderObject = SetInputsToOrderObject(customerName, area, orderDate, orderProdResponse, orderTaxResponse);
            orderObject.OrderNumber = editOrder.Order.OrderNumber;
            //save edited order
            orderRepoManager.EditOrderSave(orderObject, orderDate);
        }

        private void DeleteOrder()
        {
            //get order date and number to find the order to be deleted
            DateTime orderDate = userIO.GetDate();
            int orderNumber = userIO.ReadInt("Please put in the order number of the order you wish to delete.", 1, 100);

            //load order and end method early if order not found
            OrderResponse deleteOrder = orderRepoManager.LoadOrder(orderDate, orderNumber);
            if (deleteOrder.Success == false)
            {
                commonDisplay.DisplayErrorMessage(deleteOrder.Message);
                return;
            }

            //display order to be deleted and query whether they want to go through with the deletion
            userIO.DisplayOrder(deleteOrder.Order);
            string userConfirmation = userIO.ReadString("Are you sure you wish to delete the above order? Type yes to confirm or no to cancel");

            if (userConfirmation.ToLower() == "no")
            {
                return;
            }
            else if (userConfirmation.ToLower() == "yes")
            {
                Order oDelete = new Order();
                List<Order> keepOrders = new List<Order>();
                ListOrderResponse listOrder = orderRepoManager.LoadOrderList(orderDate);
                foreach (var item in listOrder.Orders)
                {
                    if (item.OrderNumber != orderNumber)
                    {
                        keepOrders.Add(item);
                    }
                }
                orderRepoManager.SaveOrders(keepOrders, orderDate);
            }
            else
            {
                commonDisplay.DisplayErrorMessage("User did not type in a valid response. Returning to the main menu");
            }
            
        }

        private string GetEditInput()
        {
            string userInput = userIO.ReadString("Please type the updated info you wish to change. Typing in nothing will leave the data the same as the previous entry");
            return userInput;
        }
        private int SetOrderNumber(DateTime orderDate)
        {
            int orderNumber = orderRepoManager.SetOrderNumber(orderDate);
            return orderNumber;
        }
        private Order SetInputsToOrderObject(string customerName, decimal area, DateTime orderDate,  OrderResponse orderProdResponse, OrderResponse orderTaxResponse)
        {
            Order order = new Order();
            //set all other data to order object
            order.CustomerName = customerName;
            order.Area = area;
            order.OrderDate = orderDate;
            order.CostPerSquareFoot = orderProdResponse.Order.CostPerSquareFoot;
            order.LaborCostPerSquareFoot = orderProdResponse.Order.LaborCostPerSquareFoot;
            order.ProductType = orderProdResponse.Order.ProductType;
            order.TaxRate = orderTaxResponse.Order.TaxRate;
            order.State = orderTaxResponse.Order.State;

            return order;
            //throw new NotImplementedException();
        }
    }
}
