using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers.AdminController
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class AdminAccountUserController : ControllerBase
    {
        private readonly AccountService _accountService;
        public AdminAccountUserController(AccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet("page={pageNumber}")]
        public async Task<ActionResult<IEnumerable<AccountUser>>> GetAll(int pageNumber)
        {
            try
            {
                var user = await _accountService.GetAllUser(pageNumber);
                return Ok(user);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountUser>> GetUserById(int id)
        {
            try
            {
                var user = await _accountService.GetUserById(id);
                return Ok(user);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> AddUser(AccountUser user)
        {
  
                var accountByEmail = await _accountService.GetUserByEmail(user.AccountGmail);
                var accountByName = await _accountService.GetUserByName(user.AccountName);

                if (accountByEmail != null && accountByName != null)
                {
                    return BadRequest("Tên tài khoản và Gmail đã tồn tại trong hệ thống!"); 
                }

                if (accountByEmail != null)
                {
                    return BadRequest("Gmail đã tồn tại trong hệ thống!"); 
                }

                if (accountByName != null)
                {
                    return BadRequest("Tên tài khoản đã tồn tại trong hệ thống!"); 
                }

                await _accountService.AddUser(user);
                return Ok(user);

        }


        [HttpPut]
        public async Task<ActionResult> UpdateUser(AccountUser user)
        {
            try
            {
                await _accountService.UpdateUser(user);
                return Ok(user);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                await _accountService.DeleteUser(id);
                return Ok("delete success");
            }catch (Exception ex)
            {
                return BadRequest("co loi khi xoa" + ex.Message);
            }
        }
        
    }
}
