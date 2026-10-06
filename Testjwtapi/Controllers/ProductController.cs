using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Testjwtapi.Models;

namespace Testjwtapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {

        // Temporary product list
        private static List<Product> products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Laptop",
                Price = 50000
            },
            new Product
            {
                Id = 2,
                Name = "Mouse",
                Price = 1200
            }
        };

        //[HttpGet]
        //public IActionResult GetProduct()
        //{
        //    Product product = new Product
        //    {
        //        Id = 1,
        //        Name = "LPATOP",
        //        Price = 50000
        //    };

        //    return Ok(product);
        //}


        // GET - View all products
        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(products);
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            products.Add(product);
            return Ok(product);
        }
        // PUT - Update product
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, Product product)
        {
            var existingProduct = products.FirstOrDefault(p => p.Id == id);

            if (existingProduct == null)
            {
                return NotFound("Product not found");
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;

            return Ok(existingProduct);
        }


        // DELETE - Delete product
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound("Product not found");
            }

            products.Remove(product);

            return Ok("Product deleted");
        }
    }
}
