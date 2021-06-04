
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api.Models;

namespace Api.Controllers
{
    public class ProductsController : ApiController
    {
        private List<Product> products = new List<Product>()
        {
                new Product() { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
                new Product() { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
                new Product() { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
        };
       

        [HttpGet]
        public IEnumerable<Product> GetAllProducts() 
        {
            return this.products;
        }

        [HttpGet]
        public IHttpActionResult GetProductById(int Id) 
        {
            var product = products.FirstOrDefault(x => x.Id == Id);
            if (product == null) 
            {
                return NotFound();
            }

            return Ok(product);
                
        }

        [HttpPost]
        public IHttpActionResult AddProduct([FromBody] Product product) 
        {
            if (product == null) 
            {
                return NotFound();
                
            }
            products.Add(product);
           var p = products;
            return Ok();
                    
        }
         
    }
}
