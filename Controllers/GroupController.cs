using System;
using Microsoft.AspNetCore.Mvc;
using WebBuilder.Services;
using WebBuilder.Models;

namespace WebBuilder.Controllers;

[Controller]
[Route("api/[controller]")]
public class GroupController : Controller
{
    private readonly GroupDbService _groupDbService;

    public GroupController(GroupDbService groupDbService)
    {
        _groupDbService = groupDbService;
    }

    [HttpGet]
    public async Task<List<Group>> Get() {
        return await _groupDbService.GetAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Group group) {
        await _groupDbService.CreateAsync(group);
        return CreatedAtAction(nameof(Get), new { id = group.Id}, group);
    }

    //[HttpPut("{id}")]
    //public async Task<IActionResult> AddToPlaylist(string id, [FromBody] string movieId) {
    //    await _mongoDBService.AddToPlaylistAsync(id, movieId);
    //    return NoContent();
    //}

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) {
        await _groupDbService.DeleteAsync(id);
        return NoContent();
    }
}