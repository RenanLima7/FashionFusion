using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebLuto.Services;

namespace WebLuto.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpDelete(Name = "DeleteProduct")]
        public async Task<ActionResult> Delete([FromQuery] long id)
        {
            try
            {
                //Product product = new Product();
                //product = await _productService.GetProductById(id);

                //if (product == null)
                    return NotFound();

                //await _productService.DeleteProduct(product);

                return Ok(id);

            }
            catch (Exception)
            {
                return BadRequest("Houve um erro");
            }
        }
    }
}
