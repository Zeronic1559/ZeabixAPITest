using System;
using System.Collections.Generic;

namespace Pricing.Domain.Models;

public class QuoteRequest
{
    public decimal BasePrice { get; set; }
    public decimal Weight { get; set; }
    public string Location { get; set; }

     public string Destination { get; set; }

    public DateTime RequestTime { get; set; } = DateTime.Now;
}

public class QuoteResponse
{
    public decimal BasePrice { get; set; }
    public decimal FinalPrice { get; set; }
    public List<string> AppliedRules { get; set; } = new();
}

public class JobModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Status { get; set; } = "Processing";

    public List<QuoteResponse> Results { get; set; } = new();
}