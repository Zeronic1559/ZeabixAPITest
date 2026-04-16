using Xunit;
using Pricing.Domain.Models;
using Pricing.Application;
using Pricing.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Pricing.Tests;

public class QuoteResponseModelTests
{
    [Fact]
    public void QuoteResponse_Constructor_InitializesProperties()
    {
     
        var response = new QuoteResponse();

       
        Assert.NotNull(response);
        Assert.Equal(0, response.BasePrice);
        Assert.Equal(0, response.FinalPrice);
        Assert.NotNull(response.AppliedRules);
        Assert.Empty(response.AppliedRules);
    }

    [Fact]
    public void QuoteResponse_CanSetProperties()
    {
        
        var response = new QuoteResponse
        {
            BasePrice = 100,
            FinalPrice = 150,
            AppliedRules = new List<string> { "Rule 1", "Rule 2" }
        };

       
        Assert.Equal(100, response.BasePrice);
        Assert.Equal(150, response.FinalPrice);
        Assert.Equal(2, response.AppliedRules.Count);
    }
}

public class QuoteRequestModelTests
{
    [Fact]
    public void QuoteRequest_InitializesWithValidData()
    {
       
        var request = new QuoteRequest
        {
            BasePrice = 100,
            Weight = 5.5m,
            Location = "remote",
            Destination = "International"
        };

       
        Assert.Equal(100, request.BasePrice);
        Assert.Equal(5.5m, request.Weight);
        Assert.Equal("remote", request.Location);
        Assert.Equal("International", request.Destination);
    }

    [Fact]
    public void QuoteRequest_RequestTime_DefaultsToNow()
    {
        
        var request = new QuoteRequest
        {
            BasePrice = 100
        };

       
        Assert.NotEqual(DateTime.MinValue, request.RequestTime);
        Assert.True(request.RequestTime <= DateTime.Now);
    }
}

public class JobModelTests
{
    [Fact]
    public void JobModel_InitializesWithDefaults()
    {
       
        var job = new JobModel();

        
        Assert.NotEqual(Guid.Empty, job.Id);
        Assert.Equal("Processing", job.Status);
        Assert.NotNull(job.Results);
        Assert.Empty(job.Results);
    }

    [Fact]
    public void JobModel_CanUpdateStatus()
    {
     
        var job = new JobModel();

      
        job.Status = "Completed";

       
        Assert.Equal("Completed", job.Status);
    }

    [Fact]
    public void JobModel_CanAddResults()
    {
      
        var job = new JobModel();
        var response = new QuoteResponse { FinalPrice = 150 };

      
        job.Results.Add(response);


        Assert.Single(job.Results);
        Assert.Equal(150, job.Results[0].FinalPrice);
    }
}
