using System;
using Microsoft.AspNetCore.Mvc;
using WebBuilder.Services;
using WebBuilder.Models;

namespace WebBuilder.Controllers;

[Controller]
[Route("api/[controller]")]
public class SchoolController : Controller
{
    private readonly SchoolDbService _schoolDbService;

    public SchoolController(SchoolDbService schoolDbService)
    {
        _schoolDbService = schoolDbService;
    }

    [HttpGet]
    public async Task<List<School>> Get() {
        return await _schoolDbService.GetAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] School school) {
        await _schoolDbService.CreateAsync(school);
        return CreatedAtAction(nameof(Get), new { id = school.Id}, school);
    }

    //[HttpPut("{id}")]
    //public async Task<IActionResult> AddToPlaylist(string id, [FromBody] string movieId) {
    //    await _mongoDBService.AddToPlaylistAsync(id, movieId);
    //    return NoContent();
    //}

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) {
        await _schoolDbService.DeleteAsync(id);
        return NoContent();
    }
}