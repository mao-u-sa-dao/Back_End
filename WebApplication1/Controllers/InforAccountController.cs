using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class InforAccountController : ControllerBase
    {
        private readonly InforAccountService _inforAccountService;
        public InforAccountController(InforAccountService inforAccountService)
        {
            _inforAccountService = inforAccountService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetInforAccount(int id)
        {
            var inforAccount = await _inforAccountService.GetInforAccountById(id);
            if (inforAccount == null)
            {
                return BadRequest("no have data");
            }
            return Ok(inforAccount);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateInforAccount(int id, InforAccountUser inforAccount)
        {
            try
            {
                await _inforAccountService.UpdateInforAccount(id, inforAccount);
                return Ok("update succes" + inforAccount);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
