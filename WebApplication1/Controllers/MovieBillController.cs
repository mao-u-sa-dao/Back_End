using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class MovieBillController : ControllerBase
    {
        private readonly MovieBillService _movieBillService;
        public MovieBillController(MovieBillService movieBillService)
        {
            _movieBillService = movieBillService;
        }
        [HttpPost]
        public async Task<ActionResult> AddBill(MoviesBill moviesBill)
        {
            try
            {
                await _movieBillService.AddMovieBill(moviesBill);
                return Ok(moviesBill);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
