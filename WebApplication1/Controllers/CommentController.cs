using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly CommentService _commentService;
        public CommentController(CommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetAllComments()
        {
            var comments = await _commentService.GetAllCommentAsync();
            return Ok(comments);
        }
        [HttpGet("{id}/page={pageNumber}")]
        public async Task<IActionResult> GetComments(int id, int pageNumber)
        {
            int pageSize = 5;
            var result = await _commentService.GetCommentByAsync(id, pageNumber, pageSize);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddComment(Comment comment)
        {
            try
            {
                await _commentService.AddCommentAsync(comment);
                return Ok(comment);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
