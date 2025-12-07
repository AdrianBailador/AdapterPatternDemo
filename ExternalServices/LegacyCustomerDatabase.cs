using System.Data;

namespace AdapterPatternDemo.ExternalServices;

public class LegacyCustomerDatabase
{
    public DataTable GetCustByID(int custID)
    {
        Console.WriteLine($"[Legacy DB] Fetching customer {custID}...");
        var dt = new DataTable();
        dt.Columns.Add("CustID", typeof(int));
        dt.Columns.Add("CustName", typeof(string));
        dt.Columns.Add("CustEmail", typeof(string));
        dt.Columns.Add("CustPhone", typeof(string));
        dt.Columns.Add("IsActive", typeof(int));
        dt.Rows.Add(custID, "John Doe", "john@example.com", "555-1234", 1);
        return dt;
    }

    public int UpdateCust(int custID, string name, string email, string phone)
    {
        Console.WriteLine($"[Legacy DB] Updating customer {custID}...");
        return 1;
    }
}