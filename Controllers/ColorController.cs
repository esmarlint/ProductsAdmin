using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> GetAll()
        {
            await Task.FromResult(0);
            return Ok();
        }

        public async Task<IActionResult> Create()
        {
            await Task.FromResult(0);
            return Ok();
        }

        public async Task<IActionResult> Update()
        {
            await Task.FromResult(0);
            return Ok();
        }

    }
}
