using AdapterPatternDemo.ExternalServices;
using AdapterPatternDemo.Interfaces;
using AdapterPatternDemo.Models;

namespace AdapterPatternDemo.Adapters;

public class StripePaymentAdapter : IPaymentProcessor
{
    private readonly StripeClient _stripeClient;

    public StripePaymentAdapter(StripeClient stripeClient)
    {
        _stripeClient = stripeClient;
    }

    public async Task<PaymentResult> ProcessPaymentAsync(decimal amount, string currency, string cardToken)
    {
        try
        {
            var request = new StripeChargeRequest
            {
                AmountInCents = (long)(amount * 100),
                Currency = currency.ToLowerInvariant(),
                Source = cardToken
            };

            var charge = await _stripeClient.CreateChargeAsync(request);

            return new PaymentResult(
                Success: charge.Status == "succeeded",
                TransactionId: charge.Id,
                ErrorMessage: null);
        }
        catch (Exception ex)
        {
            return new PaymentResult(false, string.Empty, ex.Message);
        }
    }

    public async Task<RefundResult> RefundAsync(string transactionId, decimal amount)
    {
        try
        {
            var amountInCents = (long)(amount * 100);
            var refund = await _stripeClient.CreateRefundAsync(transactionId, amountInCents);

            return new RefundResult(
                Success: refund.Status == "succeeded",
                RefundId: refund.Id,
                ErrorMessage: null);
        }
        catch (Exception ex)
        {
            return new RefundResult(false, string.Empty, ex.Message);
        }
    }
}