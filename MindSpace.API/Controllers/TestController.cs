using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Domain.Commons.Constants;

namespace MindSpace.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //Basic controller for testing role-based authorization
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
    }

    public record Item(int Id, string Name, bool IsComplete);
}