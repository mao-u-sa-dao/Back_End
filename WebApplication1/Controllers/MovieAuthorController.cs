﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class MovieAuthorController : ControllerBase
    {
        private readonly MovieAuthorService _movieAuthorService;
        public MovieAuthorController(MovieAuthorService movieAuthorService)
        {
            _movieAuthorService = movieAuthorService;
        }
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<MovieAuthor>>> GetAllAuthor()
        //{
        //    try { 
        //    var movieAuthor = await _movieAuthorService.GetAllAuthor();
        //    return Ok(movieAuthor);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        //[HttpGet("{id}")]
        //public async Task<ActionResult<MovieAuthor>> GetAuthorById(int id)
        //{
        //    var auThor = await _movieAuthorService.GetAuthorById(id);
        //    if (auThor == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(auThor);
        //}
        //[HttpPost]
        //public async Task<ActionResult> AddAuthor(MovieAuthor author)
        //{
        //    try
        //    {
        //        await _movieAuthorService.AddAuthor(author);
        //        return Ok(author);
        //    } catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateAuthor(int id, MovieAuthor author)
        //{
        //    try
        //    {
        //        await _movieAuthorService.UpdateAuthor(id, author);
        //        return Ok(author);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> DeleteAuthor(int id)
        //{
        //    try
        //    {
        //        await _movieAuthorService.DeleleAuthor(id);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
