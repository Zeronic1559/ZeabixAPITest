using Microsoft.AspNetCore.Mvc;
using Pricing.Application;
using Pricing.Domain.Models;
using Pricing.Domain.Interfaces;

[ApiController]
[Route("rules")]
public class RulesController : ControllerBase
{
    private readonly IRuleRepository _repo;

    public RulesController(IRuleRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_repo.GetAll());

    [HttpPost]
    public IActionResult Create([FromBody] PricingRule rule)
    {
        _repo.Add(rule);
        return Ok();
    }
}