using Ecommerce.Api.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Products.Interfaces
{
    public interface IProductProvider
    {
        Task<(bool IsSuccess, IEnumerable<ProductModel> products, string ErrorMessage)> GetProductsAsync();
    }
}
