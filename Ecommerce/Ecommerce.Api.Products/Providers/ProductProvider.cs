using AutoMapper;
using Ecommerce.Api.Products.Db;
using Ecommerce.Api.Products.Interfaces;
using Ecommerce.Api.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Products.Providers
{
    public class ProductProvider : IProductProvider
    {
        private readonly ProductsDbContext dbContext;
        private readonly ILogger<ProductProvider> logger;
        private readonly IMapper mapper;

        public ProductProvider(ProductsDbContext dbContext, ILogger<ProductProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if(!dbContext.Products.Any())
            {
                dbContext.Products.Add(new Product()
                {
                    Id = 1,
                    Inventory = 200,
                    Name = "Monitor",
                    Price = 300
                });
                dbContext.Products.Add(new Product()
                {
                    Id = 2,
                    Inventory = 400,
                    Name = "Mouse",
                    Price = 30
                });
                dbContext.Products.Add(new Product()
                {
                    Id = 3,
                    Inventory = 300,
                    Name = "Keyboard",
                    Price = 20
                });
                dbContext.Products.Add(new Product()
                {
                    Id = 4,
                    Inventory = 50,
                    Name = "Laptops",
                    Price = 2000
                });

                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<ProductModel> products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await dbContext.Products.ToListAsync();
                if (products != null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<Product>, IEnumerable<ProductModel>>(products);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            } 
            catch(Exception ex)
            {
                logger.LogError("error occurred", ex.Message);
                return (false, null, ex.Message);
            }
        }
    }
}
