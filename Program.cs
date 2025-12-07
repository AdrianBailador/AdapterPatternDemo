using AdapterPatternDemo.Adapters;
using AdapterPatternDemo.ExternalServices;
using AdapterPatternDemo.Interfaces;

Console.WriteLine("=== ADAPTER PATTERN DEMO ===\n");

// Example 1: Payment Gateway Adapter
Console.WriteLine("--- Payment Gateway Example ---");
var stripeClient = new StripeClient();
IPaymentProcessor paymentProcessor = new StripePaymentAdapter(stripeClient);

var paymentResult = await paymentProcessor.ProcessPaymentAsync(99.99m, "USD", "tok_visa_123");
Console.WriteLine($"Payment Success: {paymentResult.Success}");
Console.WriteLine($"Transaction ID: {paymentResult.TransactionId}\n");

var refundResult = await paymentProcessor.RefundAsync(paymentResult.TransactionId, 50.00m);
Console.WriteLine($"Refund Success: {refundResult.Success}");
Console.WriteLine($"Refund ID: {refundResult.RefundId}\n");

// Example 2: Legacy Database Adapter
Console.WriteLine("--- Legacy Database Example ---");
var legacyDb = new LegacyCustomerDatabase();
ICustomerRepository customerRepository = new LegacyCustomerAdapter(legacyDb);

var customer = await customerRepository.GetByIdAsync(1);
if (customer != null)
{
    Console.WriteLine($"Customer: {customer.Name}");
    Console.WriteLine($"Email: {customer.Email}");
    Console.WriteLine($"Phone: {customer.Phone}");
    Console.WriteLine($"Active: {customer.IsActive}\n");

    customer.Email = "john.updated@example.com";
    var updated = await customerRepository.UpdateAsync(customer);
    Console.WriteLine($"Update Success: {updated}");
}

Console.WriteLine("\n=== DEMO COMPLETE ===");