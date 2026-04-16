using System;
using System.Collections.Generic;
using Pricing.Domain.Interfaces;
using Pricing.Domain.Models;

namespace Pricing.Infrastructure.Repositories;

public class InMemoryJobRepository : IJobRepository
{
    private readonly Dictionary<Guid, JobModel> _jobs = new();

    public JobModel Get(Guid jobId)
    {
        return _jobs.ContainsKey(jobId) ? _jobs[jobId] : null;
    }

    public void Save(JobModel job)
    {
        _jobs[job.Id] = job;
    }

    public void Add(JobModel job)
    {
        _jobs[job.Id] = job;
    }

    public void Update(JobModel job)
    {
        _jobs[job.Id] = job;
    }
}