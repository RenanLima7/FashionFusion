using WebLuto.Models;
using WebLuto.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebLuto.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpDelete(Name = "DeleteProduct")]
        public async Task<ActionResult> DeleteProduct([FromQuery] long id)
        {
            try
            {
                Product product = new Product();
                product = await _productService.GetProductById(id);

                if (product == null)
                    return NotFound();

                await _productService.DeleteProduct(product);

                return Ok(id);

            }
            catch (Exception)
            {
                return BadRequest("Houve um erro");
            }
        }
    }
}
