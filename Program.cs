using System.Text.Json;

namespace CreditRiskManagement
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                // Reading customers from JSON file
                var jsonString = await File.ReadAllTextAsync("customers.json");
                var customers = JsonSerializer.Deserialize<List<Customer>>(jsonString);

                // Verify customer data exists before processing
                if (customers == null)
                    throw new Exception("Failed to load customer data");

                var creditScoreService = new CreditScoreService();
                var reports = new List<object>();

                // Process each customer
                foreach (var customer in customers)
                {
                    var creditScore = creditScoreService.CalculateCreditScore(customer);
                    var riskStatus = creditScoreService.DetermineRiskStatus(creditScore);

                    // Create report entry
                    var report = new
                    {
                        CustomerName = customer.Name,
                        CreditScore = creditScore,
                        RiskStatus = riskStatus
                    };
                    reports.Add(report);

                    // Logging to console
                    Console.WriteLine($"Customer: {customer.Name}");
                    Console.WriteLine($"Credit Score: {creditScore}");
                    Console.WriteLine($"Risk Status: {riskStatus}");
                    Console.WriteLine();
                }

                // Saving reports to JSON
                var reportJson = JsonSerializer.Serialize(reports, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                await File.WriteAllTextAsync("credit_reports.json", reportJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

        }
    }
}