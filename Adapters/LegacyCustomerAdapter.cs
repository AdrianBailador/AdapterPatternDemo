using AdapterPatternDemo.ExternalServices;
using AdapterPatternDemo.Interfaces;
using AdapterPatternDemo.Models;

namespace AdapterPatternDemo.Adapters;

public class LegacyCustomerAdapter : ICustomerRepository
{
    private readonly LegacyCustomerDatabase _legacyDb;

    public LegacyCustomerAdapter(LegacyCustomerDatabase legacyDb)
    {
        _legacyDb = legacyDb;
    }

    public Task<Customer?> GetByIdAsync(int id)
    {
        return Task.Run(() =>
        {
            var dataTable = _legacyDb.GetCustByID(id);

            if (dataTable.Rows.Count == 0)
                return null;

            var row = dataTable.Rows[0];

            return new Customer
            {
                Id = (int)row["CustID"],
                Name = row["CustName"]?.ToString() ?? string.Empty,
                Email = row["CustEmail"]?.ToString() ?? string.Empty,
                Phone = row["CustPhone"]?.ToString() ?? string.Empty,
                IsActive = (int)row["IsActive"] == 1
            };
        });
    }

    public Task<bool> UpdateAsync(Customer customer)
    {
        return Task.Run(() =>
        {
            var affectedRows = _legacyDb.UpdateCust(
                customer.Id,
                customer.Name,
                customer.Email,
                customer.Phone);

            return affectedRows > 0;
        });
    }
}