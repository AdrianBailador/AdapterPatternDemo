namespace AdapterPatternDemo.Models;

public record PaymentResult(
    bool Success,
    string TransactionId,
    string? ErrorMessage);

public record RefundResult(
    bool Success,
    string RefundId,
    string? ErrorMessage);