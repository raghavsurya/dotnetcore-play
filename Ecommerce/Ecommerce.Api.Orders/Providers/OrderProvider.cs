using AutoMapper;
using Ecommerce.Api.Orders.Db;
using Ecommerce.Api.Orders.Interfaces;
using Ecommerce.Api.Orders.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Orders.Providers
{
    public class OrderProvider : IOrderProvider
    {
        private readonly OrdersDbContext dbContext;
        private readonly ILogger<OrderProvider> logger;
        private readonly IMapper mapper;

        public OrderProvider(OrdersDbContext dbContext, ILogger<OrderProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Orders.Any())
            {
                dbContext.Orders.Add(new Order
                {
                    CustomerId = 100,
                    Id = 100,
                    Items = new List<OrderItem> {
                        new OrderItem{ Id = 1, OrderId = 100, ProductId = 10, Quantity = 1, UnitPrice = 10.00M}},
                    OrderDate = new DateTime(2020, 12, 1),
                    Total = 10.00M
                });

                dbContext.Orders.Add(new Order
                {
                    CustomerId = 101,
                    Id = 101,
                    Items = new List<OrderItem> {
                        new OrderItem{ Id = 2, OrderId = 101, ProductId = 20, Quantity = 10, UnitPrice = 100.00M},
                    new OrderItem { Id = 3, OrderId = 101, ProductId = 22, Quantity = 10, UnitPrice = 750.00M } 
                },
                    OrderDate = new DateTime(2020, 12, 1),
                    Total = 850.00M
                });;
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool isSuccessful, OrderModel order, string errorMessage)> GetOrderAsync(int id)
        {
            try
            {
                var orders = await dbContext.Orders.ToListAsync();
                var orderById = orders.FirstOrDefault(order => order.Id == id);
                if (orderById != null)
                {
                    var result = mapper.Map<Order,OrderModel>(orderById);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger.LogError("Error occurred", ex.Message);
                return (false, null, ex.Message);
            }
        }
        public async Task<(bool isSuccessful, IEnumerable<OrderModel> orders, string errorMessage)> GetOrdersAsync()
        {
            try
            {
                var orders = await dbContext.Orders.ToListAsync();
                if (orders != null && orders.Any())
                {
                   // orders.ForEach(order => mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemModel>>(order.Items));
                    var results = mapper.Map<IEnumerable<Order>, IEnumerable<OrderModel>>(orders);
                    return (true, results, null);
                }
                return (false, null, "Not found");
            }
            catch(Exception ex)
            {
                logger.LogError("Error occurred", ex.Message);
                return (false, null, ex.Message);
            }
        }
        public Task<(bool isSuccessful, IEnumerable<OrderItemModel> orderItems, string errorMessage)> GetOrderItemsAsync(int orderId)
        {
            throw new NotImplementedException();
        }

    }
}
