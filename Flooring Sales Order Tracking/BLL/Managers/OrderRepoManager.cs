using Assessment.Models;
using Models;
using Models.Interfaces;
using Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.RepoManagers
{
    public class OrderRepoManager
    {
        private IOrderRepository repo;
        public OrderRepoManager(IOrderRepository _repo)
        {
            repo = _repo;
        }

        //method that returns a list of orders or allows for a null list along with a bool value and error messsage to be returned
        public ListOrderResponse LoadOrderList(DateTime orderDate)
        {
            ListOrderResponse response = new ListOrderResponse();

            //get order list response from repo
            response = repo.LoadOrders(orderDate);

            return response;

            throw new NotImplementedException();
        }

        //loads a single order along with a bool and string message
        public OrderResponse LoadOrder(DateTime orderDate, int orderNumber)
        {
            OrderResponse orderResponse = new OrderResponse();

            orderResponse = repo.LoadOrder(orderDate, orderNumber);

            return orderResponse;

            throw new NotImplementedException();
        }

        //saves a single order for later retrieval
        public void SaveOrder(Order order, DateTime orderDate)
        {
            repo.SaveOrder(order, orderDate);
            //throw new NotImplementedException();
        }

        //saves multiple orders to a text document
        public void SaveOrders(List<Order> orderList, DateTime orderDate)
        {
            repo.SaveOrders(orderList, orderDate);
        }

        //method to send a true value when the date is in the future and vice versa along with appropriate messagess to send to the view
        public GetDateResponse FutureDateValidation(DateTime orderDate)
        {
            GetDateResponse response = new GetDateResponse();

            if (orderDate > DateTime.Now)
            {
                response.Success = true;
                response.OrderDate = orderDate;
                response.Message = "This is an appropriate date";

                return response;
            }
            else
            {
                response.Success = false;
                response.Message = "Please pick a date in the future";
                response.OrderDate = orderDate;

                return response;
            }
            throw new NotImplementedException();
        }

        //saving for order editing
        public void EditOrderSave(Order order, DateTime orderDate)
        {
            repo.EditOrderSave(order, orderDate);
        }

        //get order number
        public int SetOrderNumber(DateTime orderDate)
        {
            int orderNumber = repo.SetOrderNumber(orderDate);
            return orderNumber;
        }
        
    }
}
