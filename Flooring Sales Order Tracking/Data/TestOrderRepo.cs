using Assessment.Models;
using Models.Interfaces;
using Models.Responses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class TestOrderRepo : IOrderRepository
    {
        ReadWriteMethods readWrite = new ReadWriteMethods();

        ListOrderResponse response = new ListOrderResponse();

        public static List<Order> orders = new List<Order>();

        //directory for finding product data file
        DirectoryInfo dir = new DirectoryInfo(@"..\..\..\Data\Text Files\Test\Orders");

        public OrderResponse LoadOrder(DateTime orderDate, int orderNumber)
        {
            OrderResponse orderResponse = new OrderResponse();
            //add basic orders to in mem list
            if (orders.Count() == 0)
            {
                AddOrdersToInMemList();
            }

            //search static order list and return the first matching one with the same date and ordernumber
            orderResponse.Order = orders.First(x => x.OrderDate == orderDate && x.OrderNumber == orderNumber);

            //send error message if no matching order is found
            if (orderResponse.Order != null)
            {
                orderResponse.Success = true;
                return orderResponse;
            }
            else
            {
                orderResponse.Success = false;
                orderResponse.Message = "Could not find requested order. Make sure order exists or that the information was entered in correctly.";
                return orderResponse;
            }
            

            throw new NotImplementedException();
        }

        public ListOrderResponse LoadOrders(DateTime orderDate)
        {
            //create a new list that will take the static list and pull any Order objects with the supplied date
            List<Order> requestedOrders = new List<Order>();

            if (orders.Count() != 0)
            {
                //get the orders from the in mem repo witht he same date as requested and move them to the list that will be returned
                response = AddOrderObjectsToNewList(orderDate, requestedOrders);

                return response;
            }
            else
            {
                //add orders to the in mem static list
                AddOrdersToInMemList();
                //get the orders from the in mem repo witht he same date as requested and move them to the list that will be returned
                response = AddOrderObjectsToNewList(orderDate, requestedOrders);

                return response;
            }
            throw new NotImplementedException();
        }
        //for the test environment no data will be saved between program runs so we will simply add the order object to the in mem static list which will disappear after the program is exited
        public void SaveOrder(Order order, DateTime orderDate)
        {
            if (orders.Count == 0)
            {
                AddOrdersToInMemList();

            }            
                orders.Add(order);
            
            //throw new NotImplementedException();
        }

        public void SaveOrders(List<Order> orderList, DateTime orderDate)
        {
            //add range of orders to in mem list
            orders.Clear();
            orders.AddRange(orderList);
        }

        //take a line from the order text file and turn it into an object
        public Order ConvertLineToOrderObject(string line)
        {
            Order _order = new Order();

            string[] columns = line.Split(',');
            int orderNumber;
            decimal taxRate;
            decimal area;
            decimal costPerSquareFoot;
            decimal laborCostPerSquareFoot;

            if (int.TryParse(columns[0], out orderNumber))
            {
                _order.OrderNumber = orderNumber;
            }
            _order.CustomerName = columns[1];
            _order.State = columns[2];
            if (decimal.TryParse(columns[3], out taxRate))
            {
                _order.TaxRate = taxRate;
            }
            _order.ProductType = columns[4];
            if (decimal.TryParse(columns[5], out area))
            {
                _order.Area = area;
            }
            if (decimal.TryParse(columns[6], out costPerSquareFoot))
            {
                _order.CostPerSquareFoot = costPerSquareFoot;
            }
            if (decimal.TryParse(columns[7], out laborCostPerSquareFoot))
            {
                _order.LaborCostPerSquareFoot = laborCostPerSquareFoot;
            }

            return _order;

            throw new NotImplementedException();
        }

        //takes an object and converts to a delimited string so it can be put into a text file
        public string ConvertOrderObjectToLine(Order order)
        {
            string specifier = "G";
            var culture = CultureInfo.CreateSpecificCulture("eu-ES");

            string delimiter = ",";

            string line = order.OrderNumber.ToString() + delimiter + order.CustomerName + delimiter + order.State + delimiter + order.TaxRate.ToString("F") + delimiter + order.ProductType + delimiter + order.Area.ToString("F") + delimiter + order.CostPerSquareFoot.ToString("F") + delimiter + order.LaborCostPerSquareFoot.ToString("F") + delimiter + order.MaterialCost.ToString("F") + delimiter + order.LaborCost.ToString("F") + delimiter + order.Tax.ToString("F") + delimiter + order.Total.ToString("F");

            return line;
        }

        //this method is to make sure only orders with the correct date are returned from the static in mem repo
        private ListOrderResponse AddOrderObjectsToNewList(DateTime orderDate, List<Order> requestedOrders)
        {
            //loop through the in mem repo and put any orders with the same date that is supplied into the list that will be returned
            foreach (var element in orders)
            {
                if (element.OrderDate == orderDate)
                {
                    requestedOrders.Add(element);
                }
            }

            if (requestedOrders == null)
            {
                response.Success = false;
                response.Message = "No orders match the selected date. Make sure the correct date was typed in or that orders do exist on said date.";
            }
            else
            {
                response.Success = true;
                response.Orders = requestedOrders;
            }
            return response;
            throw new NotImplementedException();
        }

        private void AddOrdersToInMemList()
        {
            ListStringResponse listString = new ListStringResponse();

            string sourceFile = GetFilePath("06012013");

            //get data from text file
            listString = readWrite.GetDataFromFileWithoutHeader(sourceFile);

            //convert raw data to order list
            foreach (var element in listString.StringList)
            {
                //convert each row to an order object
                Order order = new Order();
                order = ConvertLineToOrderObject(element);
                DateTime date = new DateTime(2013, 6, 1);
                order.OrderDate = date;

                //add the newly made order object to the list of orders
                orders.Add(order);
            }
        }

        private string GetFilePath(string orderDate)
        {
            FileInfo[] textFiles = dir.GetFiles();

            foreach (var item in textFiles)
            {
                if (item.Name.Contains(orderDate))
                {
                    return item.FullName;
                }
            }
            return null;
        }

        public void EditOrderSave(Order order, DateTime orderDate)
        {
            Order replaceOrder = orders.First(x => x.OrderNumber == order.OrderNumber);
            //dont know if i need the below if statement or can just use the logic within by itself but idk if it will crash the program if I do an indexof method on a list and the search parameter is null
            if (replaceOrder != null)
            {
                int replaceIndexNum = orders.IndexOf(replaceOrder);
                orders[replaceIndexNum] = order;
                return;
            }

            /*comment out the below because the stuff above is so much nicer. Found the above when searching for whether there was a simpler replace method for lists rather than manual input
             * //use a foreach to loop through the in mem list and see if a matching order number is found. if one is found then this save is happening through the edit order method and the appropriate data points will be changed
            foreach (var item in orders)
            {
                if (item.OrderNumber == order.OrderNumber)
                {
                    item.CustomerName = order.CustomerName;
                    item.State = order.State;
                    item.ProductType = order.ProductType;
                    item.Area = order.Area;
                    item.TaxRate = order.TaxRate;
                    item.CostPerSquareFoot = order.CostPerSquareFoot;
                    item.LaborCostPerSquareFoot = order.LaborCostPerSquareFoot;

                    return;
                }
            }*/
            //add order object to the list if its a brand new order
            orders.Add(order);
            //throw new NotImplementedException();
        }

        public int SetOrderNumber(DateTime orderDate)
        {
            var dateOrders = orders.Where(x => x.OrderDate == orderDate);
            int orderNumber = dateOrders.Count() + 1;
            return orderNumber;
            throw new NotImplementedException();
        }
    }
}
