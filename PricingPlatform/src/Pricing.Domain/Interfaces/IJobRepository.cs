using System;
using Pricing.Domain.Models;

namespace Pricing.Domain.Interfaces;

public interface IJobRepository
{
    JobModel Get(Guid jobId);
    void Save(JobModel job);
    void Add(JobModel job);
    void Update(JobModel job);
}