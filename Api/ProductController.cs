using hexagonXg.Application.Models;
using hexagonXg.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        // GET: api/product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        // GET: api/product/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound(new { error = "Product not found" });
            }
            return Ok(product);
        }

        // POST: api/product
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            await _productService.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
        }

        // DELETE: api/product/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound(new { error = "Product not found" });
            }

            await _productService.DeleteProductAsync(id);
            return Ok(new { deleted = product });
        }

        // PUT: api/product/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            try
            {
                await _productService.UpdateProductAsync(product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_productService.ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(new { data = product });
        }
    }
}
