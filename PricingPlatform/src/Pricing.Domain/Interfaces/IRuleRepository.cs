using System;
using System.Collections.Generic;
using Pricing.Domain.Models;

namespace Pricing.Domain.Interfaces;

public interface IRuleRepository
{
    List<PricingRule> GetAll();
    PricingRule GetById(Guid id);
    void Add(PricingRule rule);
    void Update(PricingRule rule);
    void Delete(Guid id);
}