using AdapterPatternDemo.Models;

namespace AdapterPatternDemo.Interfaces;

public interface IPaymentProcessor
{
    Task<PaymentResult> ProcessPaymentAsync(decimal amount, string currency, string cardToken);
    Task<RefundResult> RefundAsync(string transactionId, decimal amount);
}