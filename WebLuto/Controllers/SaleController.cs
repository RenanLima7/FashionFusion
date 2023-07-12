using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebLuto.DataContract.Requests;
using WebLuto.Models;
using WebLuto.Security;
using WebLuto.Services;
using WebLuto.Services.Interfaces;
using WebLuto.Utils.Messages;

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

                object response = new
                {
                    Success = true,
                    Entity = new { saleCreated.Id, saleCreated.ClientId, saleCreated.PurchaseDate, saleCreated.TotalValue },
                    Message = "Compra realizada com sucesso!"
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
