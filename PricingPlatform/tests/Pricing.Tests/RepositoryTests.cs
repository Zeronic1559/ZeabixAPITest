using Xunit;
using Pricing.Infrastructure.Repositories;
using Pricing.Domain.Models;
using System;
using System.Collections.Generic;

namespace Pricing.Tests;

public class InMemoryJobRepositoryTests
{
    private readonly InMemoryJobRepository _repository;

    public InMemoryJobRepositoryTests()
    {
        _repository = new InMemoryJobRepository();
    }

    [Fact]
    public void Add_WithValidJob_StoresJob()
    {
        
        var jobId = Guid.NewGuid();
        var job = new JobModel
        {
            Id = jobId,
            Status = "Processing",
            Results = new List<QuoteResponse>()
        };

        
        _repository.Add(job);
        var result = _repository.Get(jobId);

       
        Assert.NotNull(result);
        Assert.Equal(jobId, result.Id);
        Assert.Equal("Processing", result.Status);
    }

    [Fact]
    public void Get_WithNonExistentId_ReturnsNull()
    {
        
        var jobId = Guid.NewGuid();

       
        var result = _repository.Get(jobId);

       
        Assert.Null(result);
    }

    [Fact]
    public void Update_WithExistingJob_UpdatesStatus()
    {
       
        var jobId = Guid.NewGuid();
        var job = new JobModel
        {
            Id = jobId,
            Status = "Processing",
            Results = new List<QuoteResponse>()
        };
        _repository.Add(job);

    
        job.Status = "Completed";
        _repository.Update(job);
        var result = _repository.Get(jobId);

        
        Assert.NotNull(result);
        Assert.Equal("Completed", result.Status);
    }

    [Fact]
    public void Save_WithValidJob_PersistsJob()
    {
        
        var job = new JobModel
        {
            Status = "Processing",
            Results = new List<QuoteResponse>()
        };

    
        _repository.Save(job);
        var result = _repository.Get(job.Id);

       
        Assert.NotNull(result);
        Assert.Equal(job.Id, result.Id);
    }

    [Fact]
    public void Add_MultipleJobs_AllStored()
    {
       
        var job1 = new JobModel { Status = "Processing" };
        var job2 = new JobModel { Status = "Completed" };

    
        _repository.Add(job1);
        _repository.Add(job2);
        var result1 = _repository.Get(job1.Id);
        var result2 = _repository.Get(job2.Id);

    
        Assert.NotNull(result1);
        Assert.NotNull(result2);
        Assert.Equal(job1.Id, result1.Id);
        Assert.Equal(job2.Id, result2.Id);
    }
}
