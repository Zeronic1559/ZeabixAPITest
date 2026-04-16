using Microsoft.AspNetCore.Mvc;
using Pricing.Application;
using Pricing.Domain.Models;
using Pricing.Domain.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;

[ApiController]
[Route("quotes")]
public class QuotesController : ControllerBase
{
    private readonly PricingService _service;
    private readonly IJobRepository _jobRepo;

    public QuotesController(PricingService service, IJobRepository jobRepo)
    {
        _service = service;
        _jobRepo = jobRepo;
    }


    [HttpPost("price")]
    public ActionResult<QuoteResponse> Price([FromBody] QuoteRequest req)
    {
        return Ok(_service.Calculate(req));
    }


    [HttpPost("bulk")]
    public ActionResult<object> Bulk([FromBody] List<QuoteRequest> requests)
    {
   
        var job = new JobModel 
        { 
            Id = Guid.NewGuid(), 
            Status = "Processing", 
            Results = new List<QuoteResponse>() 
        };

      
        _jobRepo.Add(job);

      
        _ = Task.Run(() =>
        {
            foreach (var r in requests)
            {
                var result = _service.Calculate(r);
                job.Results.Add(result);
            }
            job.Status = "Completed";
            _jobRepo.Update(job); 
        });

      
        return Ok(new { job_id = job.Id });
    }
}