using LibraryManagementSystemAPI.Publisher.Data;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemAPI.Publisher;

[ApiController]
[Route("api/[controller]")]
public class PublishersController : ControllerBase
{
    private readonly IPublisherRepository _publisherRepository;

    public PublishersController(IPublisherRepository publisherRepository)
    {
        _publisherRepository = publisherRepository;
    }

    [HttpGet]
    public async Task<ActionResult<PublisherFullInfo>> GetAllPublishers()
    {
        IEnumerable<PublisherFullInfo> publishers = await _publisherRepository.GetAllPublishers();
        return Ok(publishers);
    }
    
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<PublisherFullInfo>> GetPublisher(int id)
    {
        PublisherFullInfo? publisher = await _publisherRepository.GetPublisher(id);
        if (publisher == null)
        {
            return NotFound();
        }
        return Ok(publisher);
    }

    [HttpPost]
    public async Task<ActionResult<PublisherFullInfo>> CreatePublisher(PublisherInfo info)
    {
        var fullInfo =  await _publisherRepository.CreatePublisher(info);
        return CreatedAtAction(nameof(GetPublisher), new {fullInfo.Id}, fullInfo);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> UpdateAuthor(int id, PublisherInfo publisher)
    {
        bool updated = await _publisherRepository.UpdatePublisher(id, publisher);
        if (updated == false)
        {
            return NotFound();
        }

        return Ok();
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