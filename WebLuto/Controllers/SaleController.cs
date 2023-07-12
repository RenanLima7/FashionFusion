using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebLuto.DataContract.Requests;
using WebLuto.DataContract.Responses;
using WebLuto.Models;
using WebLuto.Security;
using WebLuto.Services;
using WebLuto.Services.Interfaces;

namespace WebLuto.Controllers
{
    [ApiController]
    [Route("api/")]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public SaleController(ISaleService saleService, IMapper mapper, ITokenService tokenService)
        {
            _saleService = saleService;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("createSale")]
        [WLToken]
        public async Task<ActionResult<dynamic>> CreateSale([FromBody] CreateSaleRequest createSaleRequest)
        {
            try
            {
                string authorizationHeader = HttpContext.Request.Headers["Authorization"];

                long clientId = _tokenService.GetUserIdFromJWTToken(authorizationHeader);

                Sale sale = _mapper.Map<Sale>(createSaleRequest);

                sale.ClientId = clientId;
                sale.ProductList = JsonSerializer.Serialize(createSaleRequest.Products);
                sale.Card = JsonSerializer.Serialize(createSaleRequest.Card);
                sale.PurchaseDate = DateTime.Now.ToUniversalTime();

                Sale saleCreated = await _saleService.Create(sale);

                List<Product> products = JsonSerializer.Deserialize<List<Product>>(saleCreated.ProductList);

                foreach (var item in products)
                {
                    item.Id = item.ProductId;
                }

                string saleId = Guid.NewGuid().ToString().Split("-")[0];

                object response = new
                {
                    Success = true,
                    Entity = new { 
                        saleId, 
                        saleCreated.ClientId, 
                        saleCreated.PurchaseDate,
                        products,
                        saleCreated.TotalValue 
                    },
                    Message = "Compra realizada com sucesso!"
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, ex.Message });
            }
        }

        [HttpGet]
        [Route("getAllSale")]
        [WLToken]
        public async Task<ActionResult<dynamic>> GetAllSale()
        {
            try
            {
                string authorizationHeader = HttpContext.Request.Headers["Authorization"];

                long clientId = _tokenService.GetUserIdFromJWTToken(authorizationHeader);

                IEnumerable<Sale> sales = await _saleService.GetAllSaleByClientId(clientId);

                List<object> saleList = new List<object>();

                foreach (var sale in sales)
                {
                    List<Product> products = JsonSerializer.Deserialize<List<Product>>(sale.ProductList);

                    foreach (var item in products)
                    {
                        item.Id = item.ProductId;
                    }

                    object saleMapped = new 
                    { 
                        sale.Id,
                        sale.TotalValue,
                        sale.PurchaseDate,
                        products
                    };

                    saleList.Add(saleMapped);
                }

                object response = new
                {
                    Success = true,
                    Entity = new
                    {
                        clientId,
                        saleList
                    },
                    Message = "Lista de compras carregada com sucesso!"
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, ex.Message });
            }
        }
    }
}
