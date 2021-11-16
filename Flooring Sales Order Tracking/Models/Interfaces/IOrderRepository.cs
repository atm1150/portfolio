using Assessment.Models;
using Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface IOrderRepository
    {
        ListOrderResponse LoadOrders(DateTime orderDate);

        OrderResponse LoadOrder(DateTime orderDate, int orderNumber);

        void SaveOrder(Order order, DateTime orderDate);

        Order ConvertLineToOrderObject(string line);

        string ConvertOrderObjectToLine(Order order);

        void SaveOrders(List<Order> orderList, DateTime orderDate);

        void EditOrderSave(Order order, DateTime orderDate);
        int SetOrderNumber(DateTime orderDate);
    }
}
