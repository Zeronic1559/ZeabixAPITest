using Microsoft.AspNetCore.Mvc;
using Pricing.Application;
using Pricing.Domain.Models;
using Pricing.Domain.Interfaces;

[ApiController]
[Route("jobs")]
public class JobsController : ControllerBase
{
    private readonly IJobRepository _repo;

    public JobsController(IJobRepository repo)
    {
        _repo = repo;
    }

    [HttpGet("{id}")]
    public ActionResult<JobModel> Get(Guid id)
    {
        var job = _repo.Get(id);
        if (job == null) return NotFound();
        return job;
    }
}