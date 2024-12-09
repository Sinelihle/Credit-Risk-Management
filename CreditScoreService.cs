using System;

public class CreditScoreService
{
    //declaring and assigning weights for credit calculation
    private const double PAYMENT_HISTORY_WEIGHT = 0.4;
    private const double CREDIT_UTILIZATION_WEIGHT = 0.3;
    private const double AGE_HISTORY_WEIGHT = 0.3;
    private const int MAX_AGE_YEARS = 10;
    private const int HIGH_RISK_THRESHOLD = 50;

    //defining function/method for calculating customer's credit score
    public int CalculateCreditScore(Customer customer)
    {
        //checking if çustomer is valid before validating
        if (customer == null)
            throw new ArgumentNullException(nameof(customer));

        //calling validation method
        ValidateCustomerData(customer);

        // Exact implementation of the formula:
        // CreditScore = (0.4 * PaymentHistory) + (0.3 * (100 - CreditUtilization)) + (0.3 * Min(AgeOfCreditHistory, 10))
        double creditScore = (PAYMENT_HISTORY_WEIGHT * customer.PaymentHistory) +
                           (CREDIT_UTILIZATION_WEIGHT * (100 - customer.CreditUtilization)) +
                           (AGE_HISTORY_WEIGHT * Math.Min(customer.AgeOfCreditHistory, MAX_AGE_YEARS));

        // Return as integer as specified in the requirements
        return (int)creditScore;
    }

    //function for retrieving customer's risk status
    public string DetermineRiskStatus(int creditScore)
    {
        //check whether score is less than threshold before returning risk status
        return creditScore < HIGH_RISK_THRESHOLD ? "High Risk" : "Low Risk";
    }

    // Validates customer data to ensure all credit-related values are within acceptable ranges.
    private void ValidateCustomerData(Customer customer)
    {
        if (customer.PaymentHistory < 0 || customer.PaymentHistory > 100)
            throw new ArgumentException("Payment history must be between 0 and 100");

        if (customer.CreditUtilization < 0 || customer.CreditUtilization > 100)
            throw new ArgumentException("Credit utilization must be between 0 and 100");

        if (customer.AgeOfCreditHistory < 0)
            throw new ArgumentException("Age of credit history cannot be negative");
    }
}