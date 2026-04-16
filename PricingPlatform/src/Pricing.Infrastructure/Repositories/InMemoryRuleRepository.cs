public class InMemoryRuleRepository : IRuleRepository
{
    private readonly List<PricingRule> _rules = new();

    public List<PricingRule> GetAll() => _rules;

    public PricingRule GetById(Guid id)
    {
        return _rules.FirstOrDefault(r => r.Id == id);
    }

    public void Add(PricingRule rule)
    {
        _rules.Add(rule);
    }

    public void Update(PricingRule rule)
    {
        var existing = GetById(rule.Id);
        if (existing != null)
        {
            var index = _rules.IndexOf(existing);
            _rules[index] = rule;
        }
    }

    public void Delete(Guid id)
    {
        var rule = GetById(id);
        if (rule != null)
        {
            _rules.Remove(rule);
        }
    }
}