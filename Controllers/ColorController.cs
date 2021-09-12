using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAdmin.Database.Contexts;
using ProductsAdmin.Database.Models;
using ProductsAdmin.Models.Api;
using ProductsAdmin.Models.Api.Requests;
using ProductsAdmin.Models.Api.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAdmin.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ColorController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            await Task.FromResult(0);

            var dbColor = context.Colors
                .Include(e => e.StatusNavigation)
                .Select(e => new ColorRestResponse
                {
                    Id = e.Id,
                    Name = e.Name,
                    Format = e.Format,
                    CreatedAt = e.CreatedAt,
                    StatusId = e.StatusNavigation.Id,
                    StatusName = e.StatusNavigation.Name,
                    Value = e.Value
                })
                .FirstOrDefault(e => e.Id == id);

            if (dbColor == null)
            {
                var errorResponse = new RestErrorResponse();
                errorResponse.ErrorType = "resource_not_found";
                errorResponse.Error = $"Color with <Id> -> <{id}> doesn´t exists";

                return NotFound(errorResponse);
            }


            var response = new RestOkResponse<ColorRestResponse>(dbColor);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            await Task.FromResult(0);

            var result = await context.Colors
                    .Include(e => e.StatusNavigation)
                .Select(e => new ColorRestResponse
                {
                    Id = e.Id,
                    Name = e.Name,
                    Format = e.Format,
                    CreatedAt = e.CreatedAt,
                    StatusId = e.StatusNavigation.Id,
                    StatusName = e.StatusNavigation.Name,
                    Value = e.Value
                }).ToListAsync();

            var response = new RestOkResponse<List<ColorRestResponse>>(result);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateColorRequest request)
        {

            try
            {
                var color = new Color();
                color.Name = request.Name;
                color.Value = request.Value;
                color.Format = request.Format;
                color.Status = request.StatusId;

                var entity = context.Colors.Add(color).Entity;
                await context.SaveChangesAsync();

                var result = context.Colors
                    .Include(e => e.StatusNavigation)
                .Select(e => new ColorRestResponse
                {
                    Id = e.Id,
                    Name = e.Name,
                    Format = e.Format,
                    CreatedAt = e.CreatedAt,
                    StatusId = e.StatusNavigation.Id,
                    StatusName = e.StatusNavigation.Name,
                    Value = e.Value
                })
                .FirstOrDefault(e => e.Id == entity.Id);

                var response = new RestOkResponse<ColorRestResponse>(result);

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

        [HttpPut]
        public async Task<IActionResult> Update()
        {
            await Task.FromResult(0);
            return Ok();
        }

    }
}
