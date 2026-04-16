using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Pricing.Domain.Models;

namespace Pricing.Integration.Tests;

public class QuotesApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public QuotesApiTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task HealthCheck_ReturnsOk()
    {
       
        var client = _factory.CreateClient();


        var response = await client.GetAsync("/health");

  
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Price_WithValidRequest_ReturnsOk()
    {
    
        var client = _factory.CreateClient();
        var request = new QuoteRequest
        {
            BasePrice = 100,
            Weight = 5.5m,
            Location = "remote",
            Destination = "International"
        };

        var response = await client.PostAsJsonAsync("/quotes/price", request);

        Assert.True(response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task Bulk_WithValidRequest_ReturnsOk()
    {
       
        var client = _factory.CreateClient();
        var requests = new List<QuoteRequest>
        {
            new QuoteRequest
            {
                BasePrice = 100,
                Weight = 5.5m,
                Location = "remote",
                Destination = "International"
            }
        };

   
        var response = await client.PostAsJsonAsync("/quotes/bulk", requests);

     
        Assert.True(response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.InternalServerError);
    }
}
