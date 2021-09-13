using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAdmin.Database.Contexts;
using ProductsAdmin.Database.Models;
using ProductsAdmin.Models.Api;
using ProductsAdmin.Models.Api.Requests;
using ProductsAdmin.Models.Api.Responses;

namespace ProductsAdmin.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductPricesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ProductPricesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id,[FromBody] UpdatePriceRequest request)
        {
            try
            {
                var price = context.ProductPrices
                        .FirstOrDefault(e => e.Id == id);

                if (price == null)
                {
                    var errorResponse = new RestErrorResponse();
                    errorResponse.ErrorType = "resource_not_found";
                    errorResponse.Error = $"Price with <Id> -> <{id}> doesn´t exists";

                    return NotFound(errorResponse);
                }

                price.Status = request.StatusId;
                price.Price = request.Price;
                price.ColorId = request.ColorId;
                price.IsDefaultPrice = request.IsDefaultPrice;

                context.Entry(price).State = EntityState.Modified;
                await context.SaveChangesAsync();

                context.Entry(price).Reference(e => e.StatusNavigation).Load();
                context.Entry(price).Reference(e => e.Color).Load();

                var result = ConvertDatabaseToRest.ConvertPriceToPriceRestResponse(price);
                var response = new RestOkResponse<PriceRestResponse>(result);

                return Ok(response);

            }
            catch (Exception e)
            {

                var errorResponse = new RestErrorResponse();
                errorResponse.ErrorType = "server_error";
                errorResponse.Error = $"Server can´t handle the request";

                return StatusCode(500, errorResponse);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProductPrice>> Create(CreatePriceRequest request)
        {
            try
            {
                var product = context.Products
                    .AsNoTracking()                    
                    .FirstOrDefault(e => e.Id == request.ProductId);

                if (product == null)
                {
                    var errorResponse = new RestErrorResponse();
                    errorResponse.ErrorType = "resource_not_found";
                    errorResponse.Error = $"Product with <Id> -> <{request.ProductId}> doesn´t exists";

                    return NotFound(errorResponse);
                }

                var price = new ProductPrice();
                price.ProductId = request.ProductId;
                price.Status = request.StatusId;
                price.Price = request.Price;
                price.ColorId = request.ColorId;
                price.IsDefaultPrice = request.IsDefaultPrice;

                context.ProductPrices.Add(price);
                await context.SaveChangesAsync();

                context.Entry(price).Reference(e => e.StatusNavigation).Load();
                context.Entry(price).Reference(e => e.Color).Load();

                var result = ConvertDatabaseToRest.ConvertPriceToPriceRestResponse(price);
                var response = new RestOkResponse<PriceRestResponse>(result);

                return StatusCode(201, response);
            }
            catch (Exception e)
            {
                var errorResponse = new RestErrorResponse();
                errorResponse.ErrorType = "server_error";
                errorResponse.Error = $"Server can´t handle the request";

                return StatusCode(500, errorResponse);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var price = context.ProductPrices
                        .FirstOrDefault(e => e.Id == id);

                if (price == null)
                {
                    var errorResponse = new RestErrorResponse();
                    errorResponse.ErrorType = "resource_not_found";
                    errorResponse.Error = $"Price with <Id> -> <{id}> doesn´t exists";

                    return NotFound(errorResponse);
                }

                price.Status = 3;

                context.Entry(price).State = EntityState.Modified;
                await context.SaveChangesAsync();

                context.Entry(price).Reference(e => e.StatusNavigation).Load();
                context.Entry(price).Reference(e => e.Color).Load();

                var result = ConvertDatabaseToRest.ConvertPriceToPriceRestResponse(price);
                var response = new RestOkResponse<PriceRestResponse>(result);

                return Ok(response);

            }
            catch (Exception e)
            {

                var errorResponse = new RestErrorResponse();
                errorResponse.ErrorType = "server_error";
                errorResponse.Error = $"Server can´t handle the request";

                return StatusCode(500, errorResponse);
            }
        }
    }
}
