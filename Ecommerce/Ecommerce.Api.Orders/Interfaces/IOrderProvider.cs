using Ecommerce.Api.Orders.Db;
using Ecommerce.Api.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Orders.Interfaces
{
    public interface IOrderProvider
    {
        Task<(bool isSuccessful, IEnumerable<OrderModel> orders, string errorMessage)> GetOrdersAsync();

        Task<(bool isSuccessful, OrderModel order, string errorMessage)> GetOrderAsync(int id);

        Task<(bool isSuccessful, IEnumerable<OrderItemModel> orderItems, string errorMessage)> GetOrderItemsAsync(int orderId);
    }
}
