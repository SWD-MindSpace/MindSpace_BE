﻿using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.Specifications.SupportingProgramSpecifications;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.SupportingPrograms;

namespace MindSpace.API.Controllers
{
    [ApiVersion(1)]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TestController : ControllerBase
    {
        private static readonly List<Item> items = new()
        {
            new Item(1, "Apple", false),
            new Item(2, "Banana", true),
            new Item(3, "Orange", false)
        };

        // GET /api/items - Get all items
        [HttpGet]
        //[AllowAnonymous] for Guest role
        [Authorize(Roles = UserRoles.Student)]
        public IActionResult GetAllItems()
        {
            return Ok(items);
        }

        // GET /api/items/{id} - Get an item by ID
        [HttpGet("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult GetItemById(int id)
        {
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // POST /api/items - Add a new item
        [HttpPost]
        [Authorize(Roles = "Doreamon")]
        public IActionResult AddItem([FromBody] Item item)
        {
            items.Add(item);
            return CreatedAtAction(nameof(GetItemById), new { id = item.Id }, item);
        }

        // ==================================
        // === Testing Purpose
        // ==================================

        [HttpGet("/testing")]
        public async Task<ActionResult<IReadOnlyList<SupportingProgram>>> GetSupportingPrograms(
            [FromQuery] SupportingProgramSpecParams supporitngProgramParams)
        {
            var spec = new SupportingProgramSpecification(supporitngProgramParams);
            //var sps = _unitOfWork.Repository<T>().GetAllAsync(spec);
            return Ok("TESTING PURPOSE");
        }
    }

    public record Item(int Id, string Name, bool IsComplete);
}