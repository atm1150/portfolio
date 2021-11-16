using Models;
using Models.Interfaces;
using Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Assessment.Models;
using System.Reflection;

namespace Data
{
    public class TestTaxRepo : ITaxesRepository
    {
        //custom constructors
        string filepath;
        public TestTaxRepo()
        {
            filepath = @"..\..\..\Data\Text Files\Test";
        }
        public TestTaxRepo(string updatedFilePath)
        {
            filepath = updatedFilePath;
        }

        ReadWriteMethods readWrite = new ReadWriteMethods();
        OrderResponse orderResponse = new OrderResponse();
        AppDomainSetup domainSetup = new AppDomainSetup();

        //there won't be multiple tax files within each run environment so only a static list is needed
        public static List<Taxes> taxes = new List<Taxes>();

        //directory for finding product data file
         //DirectoryInfo dir = new DirectoryInfo(@"..\..\..\Data\Text Files\Test");

        //DirectoryInfo dir = new DirectoryInfo(Assembly.GetEntryAssembly().FullName + \");
        //string exeAssembly = Assembly.GetEntryAssembly().FullName;
        //string taxSource = @exeAssembly + @"..\..\..\Data\Text Files\Test\Taxes.txt";


        //Loads the taxes. Has a response part included within in case I later on wish to have error messages about tax objects not parsing correctly
        public List<Taxes> LoadTaxes()
        {
            //if static list is already filled then just return it
            if (taxes.Count != 0)
            {
                return taxes;
            }

            AddTaxDataToInMemRepo();

            return taxes;

            throw new NotImplementedException();
        }

        public TaxesResponse ConvertStringToTaxObject(string taxStringData)
        {
            Taxes taxObject = new Taxes();
            TaxesResponse response = new TaxesResponse();

            string[] columns = taxStringData.Split(',');
            decimal taxRate;

            //convert string data to tax object
            taxObject.State = columns[1];
            taxObject.StateAbbreviation = columns[0];
            //if the tax rate parses correctly
            if (decimal.TryParse(columns[2], out taxRate))
            {
                taxObject.TaxRate = taxRate;
                response.Success = true;
                response.Message = "This has a tax rate";
                response.Taxes = taxObject;
            }
            else
            {
                response.Taxes = null;
                response.Success = false;
                response.Message = "The tax rate entry on the text sheet did not parse correctly. Make sure the correct number is written in the right format";
            }
            return response;

            throw new NotImplementedException();
        }

        private void AddTaxDataToInMemRepo()
        {
            TaxesResponse response = new TaxesResponse();

            string taxesSource = GetFilePath();

            //get the raw tax data form the text file
            ListStringResponse listString = readWrite.GetDataFromFileWithoutHeader(taxesSource);

            //turn each line into a tax object and add to the taxes list collection
            foreach (var element in listString.StringList)
            {
                response = ConvertStringToTaxObject(element);
                taxes.Add(response.Taxes);
            }
        }

        public OrderResponse AddTaxDataToOrderObject(string stateName)
        {
            //create order object that can have assignments made to it and then be assigned to the order response
            Order orderObject = new Order();

            if (taxes.Count == 0)
            {
                AddTaxDataToInMemRepo();
            }
            //see if any state tax data matches the state input from the program user and add the input data to order object if it does
            foreach (var item in taxes)
            {
                //lowercase state names to account for weird capitalization inputs
                stateName = stateName.ToLower();
                string lowerItem = item.State.ToLower();
                if (lowerItem == stateName)
                {
                    //set tax data to order then assign order to response
                    orderObject.TaxRate = item.TaxRate;
                    orderObject.State = item.State;
                    orderResponse.Success = true;
                    orderResponse.Order = orderObject;
                    return orderResponse;
                }
            }
            //assignments if no match is found
            orderResponse.Success = false;
            orderResponse.Message = "No match found for the input state name and the states in our database. Check spelling or if the state is in our database";
            return orderResponse;

            throw new NotImplementedException();
        }

        private string GetFilePath()
        {
            //directory for finding tax data file
            var dir = new DirectoryInfo(filepath);

            FileInfo[] textFiles = dir.GetFiles();

            foreach (var item in textFiles)
            {
                if (item.Name.Contains("Tax"))
                {
                    return item.FullName;
                }
            }
            return null;
        }
    }
}
