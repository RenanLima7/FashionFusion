using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebLuto.DataContract.Requests;
using WebLuto.DataContract.Responses;
using WebLuto.Models;
using WebLuto.Services.Interfaces;

namespace WebLuto.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        private readonly IMapper _mapper;

        private readonly ITokenService _tokenService;

        public ProductController(IProductService productService, IMapper mapper, ITokenService tokenService)
        {
            _productService = productService;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpGet]
        [Route("GetProductById")]
        public async Task<ActionResult<dynamic>> GetProductById([FromQuery] long productId)
        {
            try
            {
                Product product = await _productService.GetProductById(productId);

                if (product == null)
                    return NotFound(new { Success = false, Message = $"Não foi encontrado nenhum produto com o Id: {productId}" });
                else
                {
                    CreateProductResponse productResponse = _mapper.Map<CreateProductResponse>(product);

                    return Ok(new { Success = true, Product = productResponse });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, ex.Message });
            }
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<ActionResult<dynamic>> GetAllProducts()
        {
            try
            {
                List<Product> productList = await _productService.GetAllProducts();

                if (productList == null || productList.Count == 0)
                    return NotFound(new { Success = false, Message = $"Não foi encontrado nenhum produto" });
                else
                {
                    List<CreateProductResponse> productResponseList = new List<CreateProductResponse>();

                    foreach (Product product in productList)
                    {
                        productResponseList.Add(_mapper.Map<CreateProductResponse>(product));
                    }

                    return Ok(new { Success = true, ProductList = productResponseList });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, ex.Message });
            }
        }

        [HttpPost]
        [Route("CreateProduct")]
        public async Task<ActionResult<dynamic>> CreateProduct([FromBody] CreateProductRequest productRequest)
        {
            try
            {
                Product product = _mapper.Map<Product>(productRequest);

                Product productCreated = await _productService.CreateProduct(product);

                CreateProductResponse productResponse = _mapper.Map<CreateProductResponse>(productCreated);

                return Ok(new { Success = true, Product = productResponse });

            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, ex.Message });
            }
        }

        [HttpPut]
        [Route("UpdateProduct/{id}")]
        public async Task<ActionResult<dynamic>> UpdateProduct([FromBody] UpdateProductRequest productRequest, long id)
        {
            try
            {
                Product existingProduct = await _productService.GetProductById(id);

                if (existingProduct == null)
                    return NotFound(new { Success = false, Message = $"Não foi encontrado nenhum produto com o Id: {id}" });

                Product productToUpdated = _mapper.Map<Product>(productRequest);

                Product productUpdated = await _productService.UpdateProduct(productToUpdated, existingProduct);

                UpdateProductResponse productResponse = _mapper.Map<UpdateProductResponse>(productUpdated);

                return Ok(new { Success = true, Product = productResponse });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, ex.Message });
            }
        }

        [HttpDelete]
        [Route("DeleteProduct/{id}")]
        public async Task<ActionResult<dynamic>> DeleteProduct(long id)
        {
            try
            {
                Product existingProduct = await _productService.GetProductById(id);

                if (existingProduct == null)
                    return NotFound(new { Success = false, Message = $"Não foi encontrado nenhum produto com o Id: {id}" });

                bool successDeletedProduct = await _productService.DeleteProduct(existingProduct);

                if (successDeletedProduct)
                    return Ok(new { Success = true, Product = $"Produto {existingProduct.Id} excluído com sucesso!" });
                else
                    return BadRequest(new { Success = false, Product = $"Erro ao excluir o produto {existingProduct.Id}!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, ex.Message });
            }
        }
    }
}
