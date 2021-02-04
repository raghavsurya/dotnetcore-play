using Ecommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderProvider orderProvider;

        public OrdersController(IOrderProvider orderProvider)
        {
            this.orderProvider = orderProvider;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrdersAsync(int customerId)
        {
            var result = await orderProvider.GetOrdersAsync();
            if(result.isSuccessful)
            {
                return Ok(result.orders);
            }
            return NotFound();
        }

      
        [HttpGet("order/{id}")]
        public async Task<IActionResult> GetOrderAsync(int id)
        {
            var result = await orderProvider.GetOrderAsync(id);
            if (result.isSuccessful)
            {
                return Ok(result.order);
            }
            return NotFound();
        }
    }
}
