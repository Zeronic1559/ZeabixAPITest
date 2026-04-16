using Pricing.Domain.Interfaces;
using Pricing.Domain.Models;

namespace Pricing.Application;

public class WeightRule : IPricingRule
{
    public int Priority => 1;
    public string Name => "Weight Rule";

    public bool IsApplicable(QuoteRequest request)
    {
        return request.Weight > 0;
    }

    public decimal Apply(decimal currentPrice, QuoteRequest request)
    {
        return currentPrice + (request.Weight * 10m);
    }
}

public class RemoteRule : IPricingRule
{
    public int Priority => 2;
    public string Name => "Remote Area";

    public bool IsApplicable(QuoteRequest request)
    {
        return request.Destination == "REMOTE";
    }

    public decimal Apply(decimal currentPrice, QuoteRequest request)
    {
        return currentPrice + 50m;
    }
}

public class TimeRule : IPricingRule
{
    public int Priority => 3;
    public string Name => "Peak Hour";

    public bool IsApplicable(QuoteRequest request)
    {
        return request.RequestTime.Hour >= 18;
    }

    public decimal Apply(decimal currentPrice, QuoteRequest request)
    {
        return currentPrice + 20m;
    }
}