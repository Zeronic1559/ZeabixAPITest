
using Microsoft.AspNetCore.Mvc;
using Pricing.Application;
using Pricing.Domain.Interfaces;
using Pricing.Domain.Models;

[ApiController]
[Route("health")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok("OK");
}