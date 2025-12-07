namespace AdapterPatternDemo.ExternalServices;

public class StripeClient
{
    public async Task<StripeCharge> CreateChargeAsync(StripeChargeRequest request)
    {
        await Task.Delay(100);
        Console.WriteLine($"[Stripe] Creating charge for {request.AmountInCents} cents...");
        return new StripeCharge
        {
            Id = $"ch_{Guid.NewGuid():N}",
            Status = "succeeded",
            Amount = request.AmountInCents
        };
    }

    public async Task<StripeRefund> CreateRefundAsync(string chargeId, long amountInCents)
    {
        await Task.Delay(100);
        Console.WriteLine($"[Stripe] Creating refund for charge {chargeId}...");
        return new StripeRefund
        {
            Id = $"re_{Guid.NewGuid():N}",
            Status = "succeeded"
        };
    }
}

public class StripeChargeRequest
{
    public long AmountInCents { get; set; }
    public string Currency { get; set; } = "usd";
    public string Source { get; set; } = string.Empty;
}

public class StripeCharge
{
    public string Id { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public long Amount { get; set; }
}

public class StripeRefund
{
    public string Id { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}