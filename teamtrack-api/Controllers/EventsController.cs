using Microsoft.AspNetCore.Mvc;
using teamtrack_api.Model;

namespace teamtrack_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase {

    private readonly DataContext context;

    public EventsController(DataContext c)  {
        context = c;
    }

    [HttpGet]
    public IActionResult GetEvents() {
        return Ok(context.EventList);
    }   

    [HttpPost]
    public IActionResult Create([FromBody] Event e) {
        var dbEvent = context.EventList?.Find(e.Id); 
        if (dbEvent == null) {
            context.EventList?.Add(e); 
            context.SaveChanges();
            return CreatedAtAction(nameof(GetEvents), new { e.Id }, e);
        }

        return Conflict();

    }

    [HttpPut("{id}")]
    public IActionResult Update(int? id, [FromBody] Event e) {
        if (id != e.Id || !context.EventList!.Any(e => e.Id == id)) return NotFound();
        context.Update(e);
        context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id) {
        var eventToDelete = context.EventList?.Find(id);
        if (eventToDelete == null) return NotFound();
        context.EventList?.Remove(eventToDelete);
        context.SaveChanges();
        return NoContent();
    }
}

