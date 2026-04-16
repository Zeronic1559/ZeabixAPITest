using System;

namespace Pricing.Domain.Models;

public class PricingRule
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Priority { get; set; }
    public DateTime EffectiveFrom { get; set; }
    public DateTime EffectiveTo { get; set; }
    public bool IsActive { get; set; }
}