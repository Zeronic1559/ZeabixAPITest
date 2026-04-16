using Swashbuckle.AspNetCore.Filters;
using Pricing.Domain.Models;

namespace Pricing.API.Swagger;

public class QuoteRequestExample : IExamplesProvider<QuoteRequest>
{
    public QuoteRequest GetExamples()
    {
        return new QuoteRequest
        {
            BasePrice = 100,
            Weight = 5.5m,
            Location = "remote",
            Destination = "International",
            RequestTime = DateTime.UtcNow.AddHours(1)
        };
    }
}

public class QuoteResponseExample : IExamplesProvider<QuoteResponse>
{
    public QuoteResponse GetExamples()
    {
        return new QuoteResponse
        {
            BasePrice = 100,
            FinalPrice = 127.5m,
            AppliedRules = new List<string>
            {
                "Weight Rule: +10%",
                "Remote Location: +15%"
            }
        };
    }
}