using Pricing.Domain.Models;

namespace Pricing.Domain.Interfaces;

public interface IPricingRule
{
    bool IsApplicable(QuoteRequest request);
    decimal Apply(decimal currentPrice, QuoteRequest request);
    int Priority { get; }
    string Name { get; }
}