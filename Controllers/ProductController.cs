using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductsAdmin.Database.Contexts;
using ProductsAdmin.Database.Models;
using ProductsAdmin.Extensions;
using ProductsAdmin.Models.Api;
using ProductsAdmin.Models.Api.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAdmin.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IConfiguration configuration;

        public ProductController(ApplicationDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParameters paginationParameters)
        {
            try
            {
                await Task.FromResult(0);
                if (paginationParameters.Page <= 0)
                {
                    paginationParameters.Page = configuration.GetValue<int>("Pagination:DefaultPage");
                }

                if (paginationParameters.PageSize <= 0)
                {
                    paginationParameters.PageSize = configuration.GetValue<int>("Pagination:PageLimit");
                }

                var products = context.Products
                        .Include(e => e.StatusNavigation)
                        .Include(e => e.ProductPrices)
                            .ThenInclude(e => e.StatusNavigation)
                        .Include(e => e.ProductPrices)
                            .ThenInclude(e => e.Color)
                    .AsNoTracking()
                    .AsQueryable()
                    .Paginate(paginationParameters.Page, paginationParameters.PageSize)
                    .Select(ConvertDatabaseToRest.ConvertProductToProductRestResponse)
                    .ToList();

                var response = new RestPaginatedResponse<ProductRestResponse>(products, paginationParameters);

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                await Task.FromResult(0);
                var product = context.Products
                    .Include(e => e.StatusNavigation)
                    .Include(e => e.ProductPrices)
                        .ThenInclude(e => e.StatusNavigation)
                    .Include(e => e.ProductPrices)
                        .ThenInclude(e => e.Color)
                .AsNoTracking()
                .Select(ConvertDatabaseToRest.ConvertProductToProductRestResponse)
                .FirstOrDefault(e => e.Id == id);

                if (product == null)
                {
                    var errorResponse = new RestErrorResponse();
                    errorResponse.ErrorType = "resource_not_found";
                    errorResponse.Error = $"Product with <Id> -> <{id}> doesn´t exists";

                    return NotFound(errorResponse);
                }

                var response = new RestOkResponse<ProductRestResponse>(product);

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
