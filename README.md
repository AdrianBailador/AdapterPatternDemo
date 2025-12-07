# Adapter Pattern Demo

A practical demonstration of the **Adapter Pattern** in C# with real-world examples: Payment Gateway integration and Legacy Database adaptation.

## 🎯 What is the Adapter Pattern?

The Adapter Pattern converts the interface of a class into another interface that clients expect. It allows classes to work together that couldn't otherwise because of incompatible interfaces.

```
┌─────────────┐      ┌─────────────┐      ┌─────────────┐
│  Interface  │ ───▶ │   ADAPTER   │ ───▶ │   Adaptee   │
│  (Target)   │      │             │      │  (Legacy)   │
└─────────────┘      └─────────────┘      └─────────────┘
```

## 📁 Project Structure

```
AdapterPatternDemo/
├── Adapters/
│   ├── StripePaymentAdapter.cs    # Adapts Stripe SDK to IPaymentProcessor
│   └── LegacyCustomerAdapter.cs   # Adapts Legacy DB to ICustomerRepository
├── Interfaces/
│   ├── IPaymentProcessor.cs       # Target interface for payments
│   └── ICustomerRepository.cs     # Target interface for customers
├── Models/
│   ├── PaymentModels.cs           # PaymentResult, RefundResult
│   └── Customer.cs                # Customer entity
├── ExternalServices/
│   ├── StripeClient.cs            # Simulated Stripe SDK (Adaptee)
│   └── LegacyCustomerDatabase.cs  # Legacy system with DataTable (Adaptee)
└── Program.cs                     # Demo application
```

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download) or later

### Run the Demo

```bash
git clone https://github.com/adrianbailador/AdapterPatternDemo.git
cd AdapterPatternDemo
dotnet run
```

### Expected Output

```
=== ADAPTER PATTERN DEMO ===

--- Payment Gateway Example ---
[Stripe] Creating charge for 9999 cents...
Payment Success: True
Transaction ID: ch_1ad96815510941eab712f734f2a6a577

[Stripe] Creating refund for charge ch_1ad96815510941eab712f734f2a6a577...
Refund Success: True
Refund ID: re_f75bad8c52ef4d8986ee1a34a53f9406

--- Legacy Database Example ---
[Legacy DB] Fetching customer 1...
Customer: John Doe
Email: john@example.com
Phone: 555-1234
Active: True

[Legacy DB] Updating customer 1...
Update Success: True

=== DEMO COMPLETE ===
```

## 💡 Examples Explained

### Example 1: Payment Gateway Adapter

**Problem:** Your application uses `IPaymentProcessor` with decimal amounts, but Stripe's SDK uses cents (long).

**Solution:** `StripePaymentAdapter` converts between the two interfaces.

```csharp
// Your code works with decimals
IPaymentProcessor processor = new StripePaymentAdapter(stripeClient);
await processor.ProcessPaymentAsync(99.99m, "USD", "tok_visa");

// Adapter converts to Stripe's format (cents)
// 99.99 → 9999 cents
```

### Example 2: Legacy Database Adapter

**Problem:** Your modern application uses `async/await` and strongly-typed `Customer` objects, but the legacy system uses synchronous methods and `DataTable`.

**Solution:** `LegacyCustomerAdapter` bridges the gap.

```csharp
// Your code works with modern patterns
ICustomerRepository repository = new LegacyCustomerAdapter(legacyDb);
Customer? customer = await repository.GetByIdAsync(1);

// Adapter handles the conversion
// DataTable → Customer object
// Sync → Async
```

## 🔑 Key Components

| Component | Role | Description |
|-----------|------|-------------|
| **Target** | Interface | What your application expects (`IPaymentProcessor`, `ICustomerRepository`) |
| **Adaptee** | External/Legacy | The incompatible class (`StripeClient`, `LegacyCustomerDatabase`) |
| **Adapter** | Bridge | Converts Target calls to Adaptee calls |
| **Client** | Consumer | Your application code that uses the Target interface |

## ✅ When to Use

- Integrating third-party libraries with different interfaces
- Working with legacy systems that can't be modified
- Migrating between services (e.g., switching payment providers)
- Supporting multiple implementations of similar functionality

## ❌ When to Avoid

- You control both interfaces — refactor instead
- Simple mappings — use extension methods or AutoMapper
- One-time integrations — overhead might not be worth it

## 📚 Related Patterns

| Pattern | Purpose | Difference |
|---------|---------|------------|
| **Adapter** | Makes incompatible interfaces work together | Converts interface |
| **Facade** | Simplifies a complex subsystem | Provides simpler interface |
| **Decorator** | Adds behavior to objects | Keeps same interface |
| **Bridge** | Separates abstraction from implementation | Designed upfront |


## 👤 Author

**Adrian Bailador Panero**

- Blog: [adrianbailador.github.io](https://adrianbailador.github.io)
- GitHub: [@adrianbailador](https://github.com/adrianbailador)
- LinkedIn: [Adrian Bailador](https://www.linkedin.com/in/adrianbailador/)

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

