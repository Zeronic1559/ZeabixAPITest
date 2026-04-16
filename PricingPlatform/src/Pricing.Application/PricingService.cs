using Pricing.Domain.Interfaces;
using Pricing.Domain.Models;
using System.Collections.Generic;

namespace Pricing.Application;

public class PricingService
{
    private readonly IEnumerable<IPricingRule> _rules;

    public PricingService(IEnumerable<IPricingRule> rules)
    {
        _rules = rules;
    }

    public QuoteResponse Calculate(QuoteRequest request)
    {
        decimal price = request.BasePrice;

        foreach (var rule in _rules)
        {
            price = rule.Apply(price, request);
        }

        return new QuoteResponse
        {
            FinalPrice = price
        };
    }
}