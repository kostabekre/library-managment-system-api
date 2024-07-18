using LibraryManagementSystemAPI.Models;
using LibraryManagementSystemAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemAPI.Controllers;

[ApiController]
[Route("api/{controller}")]
public class PublishersController : ControllerBase
{
    private readonly IPublisherRepository _publisherRepository;

    public PublishersController(IPublisherRepository publisherRepository)
    {
        _publisherRepository = publisherRepository;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Publisher>> GetPublisher(int id)
    {
        Publisher? publisher = await _publisherRepository.GetPublisher(id);
        if (publisher == null)
        {
            return NotFound();
        }
        return Ok(publisher);
    }

    [HttpPost]
    public async Task<ActionResult<Publisher>> CreatePublisher(Publisher publisher)
    {
        await _publisherRepository.CreatePublisher(publisher);
        return CreatedAtAction(nameof(GetPublisher), publisher);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<Publisher>> UpdateAuthor(int id, Publisher publisher)
    {
        bool updated = await _publisherRepository.UpdatePublisher(id, publisher);
        if (updated == false)
        {
            return NotFound();
        }

        return publisher;
    }
    

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeletePublisher(int id)
    {
        bool deleted = await _publisherRepository.DeletePublisher(id);
        if (deleted == false)
        {
            return NotFound();
        }

        return Ok();
    }
}