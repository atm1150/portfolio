using Assessment.Models;
using Models;
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
    public class TestProductRepo : IProductRepository
    {
        ReadWriteMethods readWrite = new ReadWriteMethods();
        OrderResponse orderResponse = new OrderResponse();

        //make it static as there will never be more than one product list
        public static List<Product> products = new List<Product>();

        //directory for finding product data file
        DirectoryInfo dir = new DirectoryInfo(@"..\..\..\Data\Text Files\Test");
        
        //ignore the stuff below this comment. was there as a reference while figuring out relative pathing
        //DirectoryInfo DirectoryInfo = new DirectoryInfo(@"..\..\..\online - net - atm1150\OOP_Assessment\Data\Text Files\Test\Products.txt");

        public List<Product> LoadProducts()
        {
            //if static list is already filled then just return it
            if (products.Count != 0)
            {
                return products;
            }

            AddProductsToInMemList();
            
            return products;

            throw new NotImplementedException();
        }

        public Product ConvertStringToProductObject(string productStringData)
        {
            Product productObject = new Product();
            
            //split each line into separate strings for data separation
            string[] columns = productStringData.Split(',');

            //set up non string variable for data
            decimal costPerSquareFoot;
            decimal laborCostPerSquareFoot;

            //convert raw string data to Product object
            productObject.ProductType = columns[0];
            if (decimal.TryParse(columns[1], out costPerSquareFoot))
            {
                productObject.CostPerSquareFoot = costPerSquareFoot;
            }
            if (decimal.TryParse(columns[2], out laborCostPerSquareFoot))
            {
                productObject.LaborCostPerSquareFoot = laborCostPerSquareFoot;
            }
            return productObject;

        }

        public OrderResponse AddProductDataToOrderObject(string productName)
        {
            if (products.Count == 0)
            {
                AddProductsToInMemList();
            }

            //see if any products match the product entered or end the method early if none do
            foreach (var item in products)
            {
                //create reference for an order object
                Order orderObject = new Order();

                //lower both product name strings to make sure bad capitalization does not interfere with the program
                productName = productName.ToLower();
                string lowerItem = item.ProductType.ToLower();

                //set the relevant data to the new order object to be added
                if (lowerItem == productName)
                {
                    //set new order product details then set order to order response
                    orderObject.ProductType = item.ProductType;
                    orderObject.LaborCostPerSquareFoot = item.LaborCostPerSquareFoot;
                    orderObject.CostPerSquareFoot = item.CostPerSquareFoot;
                    orderResponse.Success = true;
                    orderResponse.Order = orderObject;
                    return orderResponse;
                }
            }

            //assignments if no match is found
            orderResponse.Success = false;
            orderResponse.Message = "No match found to given input. Please check spelling or that database has said product within";
            return orderResponse;

            throw new NotImplementedException();
        }

        private void AddProductsToInMemList()
        {
            ListStringResponse response = new ListStringResponse();

            string filePath = GetFilePath();

            //get raw product data from text file
            response = readWrite.GetDataFromFileWithoutHeader(filePath);

            //turn each line into a product object and add to the list collection
            foreach (var element in response.StringList)
            {
                Product productObject = ConvertStringToProductObject(element);
                products.Add(productObject);
            }
        }

        private string GetFilePath()
        {
            FileInfo[] textFiles = dir.GetFiles();

            foreach (var item in textFiles)
            {
                if (item.Name.Contains("Prod"))
                {
                    return item.FullName;
                }
            }
            return null;
        }

        //only putting this here to figure out how to get a relative path to a text file
        public string GetRelativePath(string relativeTo, string path)
        {
            var uri = new Uri(relativeTo);
            var rel = Uri.UnescapeDataString(uri.MakeRelativeUri(new Uri(path)).ToString()).Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            if (rel.Contains(Path.DirectorySeparatorChar.ToString()) == false)
            {
                rel = $".{ Path.DirectorySeparatorChar }{ rel }";
            }
            return rel;
        }
    }
}
//   C:\Repos\online-net-atm1150\OOP_Assessment\Console.App\bin\Data\Text Files\Test'.'