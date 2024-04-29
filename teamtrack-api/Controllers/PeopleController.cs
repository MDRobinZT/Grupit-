using Microsoft.AspNetCore.Mvc;
using teamtrack_api.Model;

namespace teamtrack_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleController : ControllerBase {

    private readonly DataContext context;

    public PeopleController(DataContext c)  {
        context = c;
    }

    [HttpGet]
    public IActionResult GetPeople() {
        return Ok(context.PersonList);
    }   

    [HttpPost]
    public IActionResult Create([FromBody] Person p) {
        var dbPerson = context.PersonList?.Find(p.Id); 
        if (dbPerson == null) {
            context.PersonList?.Add(p); 
            context.SaveChanges();
            return CreatedAtAction(nameof(GetPeople), new { p.Id }, p);
        }

        return Conflict();

    }

    [HttpPut("{id}")]
    public IActionResult Update(int? id, [FromBody] Person p) {
        if (id != p.Id || !context.PersonList!.Any(p => p.Id == id)) return NotFound();
        context.Update(p);
        context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id) {
        var PersonToDelete = context.PersonList?.Find(id);
        if (PersonToDelete == null) return NotFound();
        context.PersonList?.Remove(PersonToDelete);
        context.SaveChanges();
        return NoContent();
    }
}

