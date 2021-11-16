using Models.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ReadWriteMethods
    {
        //method to create file name based on the order date for use in file creation and file path setting
        public string CreateFileName(DateTime orderDate)
        {
            string dateString = orderDate.ToString("MMddyyyy");

            string fileName = "Orders_" + dateString + ".txt";

            return fileName;
        }

        //method to read a given text file and put the data on the text file into a string array without the header
        public ListStringResponse GetDataFromFileWithoutHeader(string path)
        {
            ListStringResponse response = new ListStringResponse();

            if (File.Exists(path))
            {
                string[] rows = File.ReadAllLines(path);

                //get rid of the first element of the array  by first turning it to a list so there is no null value in any element
                List<string> orderList = rows.ToList();
                if (orderList.Count == 0)
                {
                    response.Success = false;
                    return response;
                }
                orderList.RemoveAt(0);

                response.StringList = orderList;
                response.Success = true;

                return response;
            }
            else
            {
                response.Success = false;
                response.Message = "No file corresponds with the date typed in. Please try a different date or make sure the orders are input into the correct date.";

                return response;
            }

            
        }

        public void WriteOrderToNewFile(string order, string path)
        {
            string headerRow = "OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total";

            string[] textDocArray = new string[] { headerRow, order};

            File.WriteAllLines(path, textDocArray);
            
            /*This section below doesn't work because when the streamwriter is used it says the file is in use elsewhere. ask instructor about it
            //create file
            File.Create(path);

            //add header
            using (StreamWriter writer = File.AppendText(path))
            {
                writer.WriteLine(headerRow);
            }

            //add order
            using (StreamWriter writer = File.AppendText(path))
            {
                writer.WriteLine(order);
            }
            */
        }

        public void WriteOrderToExistingDateFile(string order, string path)
        {
            using (StreamWriter writer = File.AppendText(path))
            {
                writer.WriteLine(order);
            }

            //throw new NotImplementedException();
        }

        public void WriteAllToFile(List<string> orderList, string path)
        {
            string headerRow = "OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total";

            orderList.Insert(0, headerRow);

            string[] ordersArray = orderList.ToArray();

            File.WriteAllLines(path, ordersArray);
        }
    }
}
