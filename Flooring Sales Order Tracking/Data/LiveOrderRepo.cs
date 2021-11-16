using Assessment.Models;
using Models.Interfaces;
using Models.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class LiveOrderRepo : IOrderRepository
    {
        ListOrderResponse response = new ListOrderResponse();
        ReadWriteMethods readWrite = new ReadWriteMethods();

        //directory for finding product data file
        DirectoryInfo dir = new DirectoryInfo(@"..\..\..\Data\Text Files\Live\Orders");

        public OrderResponse LoadOrder(DateTime orderDate, int orderNumber)
        {
            OrderResponse orderResponse = new OrderResponse();

            //change orderNumber to string for later use
            string oNumber = orderNumber.ToString();

            //get file name
            string fileName = readWrite.CreateFileName(orderDate);

            //get file path
            string textPath = GetFilePath(fileName);
            if (textPath == null)
            {
                orderResponse.Success = false;
                orderResponse.Message = "Could not find requested order file. Check to make sure text file exists or that the date was input correctly.";
                return orderResponse;
            }

            //get data from text file
            ListStringResponse orderTextData = readWrite.GetDataFromFileWithoutHeader(textPath);

            //comparison list for finding correct order number
           // List<string> compareList = new List<string>();

            //convert raw data to order list
            foreach (var element in orderTextData.StringList)
            {
                string str = element.Remove(1);

                if (str == oNumber)
                {
                    //convert row to an order object
                    Order order = new Order();
                    order = ConvertLineToOrderObject(element);
                    order.OrderDate = orderDate;
                    orderResponse.Order = order;
                    orderResponse.Success = true;
                    return orderResponse;
                }
            }
            //assignments if no order number matches
            orderResponse.Success = false;
            orderResponse.Message = "No matching order number was found. Check to see if said order number exists or if the number was typed incorrectly.";
            return orderResponse;

            throw new NotImplementedException();
        }

        public ListOrderResponse LoadOrders(DateTime orderDate)
        {
            //create a new list to be returned within the response
            List<Order> requestedOrders = new List<Order>();

            //get file name
            string fileName = readWrite.CreateFileName(orderDate);

            //get file path
            string textPath = GetFilePath(fileName);

            //get data from text file
            ListStringResponse orderTextData = readWrite.GetDataFromFileWithoutHeader(textPath);

            if (orderTextData != null)
            {
                //convert raw data to order list
                foreach (var element in orderTextData.StringList)
                {
                    //convert each row to an order object
                    Order order = new Order();
                    order = ConvertLineToOrderObject(element);
                    order.OrderDate = orderDate;
                    requestedOrders.Add(order);
                }
            }
            
            //if the file was found
            if (orderTextData.Success)
            {
                response.Orders = requestedOrders;
                response.Success = true;
                return response;
            }
            else
            {
                response.Success = false;
                response.Message = orderTextData.Message;
                return response;
            }
            
            throw new NotImplementedException();
        }

        public void SaveOrder(Order order, DateTime orderDate)
        {
            string fileName = readWrite.CreateFileName(orderDate);
            string stringOrder = ConvertOrderObjectToLine(order);

            string textPath = GetFilePath(fileName);
            if (textPath == null)
            {
                textPath = @"..\..\..\Data\Text Files\Live\Orders\" + @fileName;
                readWrite.WriteOrderToNewFile(stringOrder, textPath);
                return;
            }
            else
            {
              /*  ListStringResponse stringresponse = readWrite.GetDataFromFileWithoutHeader(textPath);
                List<string> listString = stringresponse.StringList;

                if (stringresponse.Success)
                {
                    //code for replacing an existing order
                    string replaceOrder = listString.First(x => x.Contains(order.OrderNumber.ToString()));
                    if (replaceOrder != null)
                    {
                        int replaceIndexNum = listString.IndexOf(replaceOrder);
                        listString[replaceIndexNum] = replaceOrder;

                        //delete file to make sure no data is doubled up and then write all information to recreated blank file
                        File.Delete(textPath);
                        readWrite.WriteAllToFile(listString, textPath);
                        return;
                    }
                }*/

                //code for saving if no data was in text file but text file had already been created
                readWrite.WriteOrderToExistingDateFile(stringOrder, textPath);
                
            }
           // throw new NotImplementedException();
        }

        public Order ConvertLineToOrderObject(string line)
        {
            Order _order = new Order();

            string[] columns = line.Split(',');
            int orderNumber;
            decimal taxRate;
            decimal area;
            decimal costPerSquareFoot;
            decimal laborCostPerSquareFoot;

            if (decimal.TryParse(columns[7], out laborCostPerSquareFoot))
            {
                _order.LaborCostPerSquareFoot = laborCostPerSquareFoot;
            }
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

            return _order;

            throw new NotImplementedException();
        }

        //takes an object and converts to a delimited string so it can be put into a text file
        public string ConvertOrderObjectToLine(Order order)
        {
            string delimiter = ",";


            string line = order.OrderNumber.ToString() + delimiter + order.CustomerName + delimiter + order.State + delimiter + order.TaxRate.ToString() + delimiter + order.ProductType + delimiter + order.Area.ToString("F") + delimiter + order.CostPerSquareFoot.ToString("F") + delimiter + order.LaborCostPerSquareFoot.ToString("F") + delimiter + order.MaterialCost.ToString("F") + delimiter + order.LaborCost.ToString("F") + delimiter + order.Tax.ToString("F") + delimiter + order.Total.ToString("F");

            return line;
        }

        private string GetFilePath(string fileDate)
        {
            FileInfo[] textFiles = dir.GetFiles();

            foreach (var item in textFiles)
            {
                if (item.Name.Contains(fileDate))
                {
                    return item.FullName;
                }
            }
            return null;
        }

        public void SaveOrders(List<Order> orderList, DateTime orderDate)
        {
            string fileName = readWrite.CreateFileName(orderDate);
            List<string> stringOrders = new List<string>();
            foreach (var item in orderList)
            {
                string dataString = ConvertOrderObjectToLine(item);
                stringOrders.Add(dataString);
            }

            string textPath = GetFilePath(fileName);

            if (textPath == null)
            {
                textPath = @"..\..\..\Data\Text Files\Live\Orders\" + @fileName;
                readWrite.WriteAllToFile(stringOrders, textPath);
            }
            else
            {
                //if the file already exists then delete it and remake it to ensure no doubled up data
                File.Delete(textPath);

                textPath = @"..\..\..\Data\Text Files\Live\Orders\" + @fileName;
                readWrite.WriteAllToFile(stringOrders, textPath);
            }
        }

        public void EditOrderSave(Order order, DateTime orderDate)
        {
            string fileName = readWrite.CreateFileName(orderDate);
            string stringOrder = ConvertOrderObjectToLine(order);

            string textPath = GetFilePath(fileName);
            if (textPath == null)
            {
                textPath = @"..\..\..\Data\Text Files\Live\Orders\" + @fileName;
                readWrite.WriteOrderToNewFile(stringOrder, textPath);
                return;
            }
            else
            {
                ListStringResponse stringresponse = readWrite.GetDataFromFileWithoutHeader(textPath);
                List<string> listString = stringresponse.StringList;

                //creating a simplelist that only contains the first character of each order from the list string so that the later replace order won't accidentally trigger if the same number is elsewhere in the string other than in the order number delimited section
                List<string> simpleList = new List<string>();
                foreach (var item in listString)
                {
                    string comparison = item.Remove(1);
                    simpleList.Add(comparison);
                }

                if (stringresponse.Success)
                {
                    //code for replacing an existing order
                    
                    //Below line would bring back the first string that hada number anywhere within that was the same as the order number
                    string replaceOrder = simpleList.First(x => x.Contains(order.OrderNumber.ToString()));
                    
                    if (replaceOrder != null)
                    {
                        int replaceIndexNum = simpleList.IndexOf(replaceOrder);
                        listString[replaceIndexNum] = stringOrder;

                        //delete file to make sure no data is doubled up and then write all information to recreated blank file
                        File.Delete(textPath);
                        readWrite.WriteAllToFile(listString, textPath);
                        return;
                    }
                }

                //code for saving if no data was in text file but text file had already been created
                readWrite.WriteOrderToNewFile(stringOrder, textPath);

            }
        }

        public int SetOrderNumber(DateTime orderDate)
        {
            int orderNumber;
            //get file name
            string fileName = readWrite.CreateFileName(orderDate);

            //get file path
            string textPath = GetFilePath(fileName);

            //get data from text file
            ListStringResponse orderTextData = readWrite.GetDataFromFileWithoutHeader(textPath);

            //if no file exists
            if (orderTextData.StringList == null)
            {
                orderNumber = 1;
                return orderNumber;
            }
            orderNumber = orderTextData.StringList.Count() + 1;
            return orderNumber;
        }
    }
}
