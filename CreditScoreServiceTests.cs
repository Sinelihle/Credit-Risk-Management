using Xunit;

public class CreditScoreServiceTests
{
    private readonly CreditScoreService _service;

    public CreditScoreServiceTests()
    {
        _service = new CreditScoreService();
    }

    [Fact]
    public void CalculateCreditScore_ValidCustomer_ReturnsCorrectScore()
    {
        // Arrange
        var customer = new Customer
        {
            Name = "Test Customer",
            PaymentHistory = 90,
            CreditUtilization = 40,
            AgeOfCreditHistory = 5
        };

        // Act
        var score = _service.CalculateCreditScore(customer);

        // Assert
        Assert.Equal(71, score);
    }

    [Theory]
    [InlineData(-1, 50, 5)]
    [InlineData(101, 50, 5)]
    [InlineData(90, -1, 5)]
    [InlineData(90, 101, 5)]
    [InlineData(90, 50, -1)]
    public void CalculateCreditScore_InvalidData_ThrowsArgumentException(
        double paymentHistory,
        double creditUtilization,
        double ageOfCreditHistory)
    {
        // Arrange
        var customer = new Customer
        {
            PaymentHistory = paymentHistory,
            CreditUtilization = creditUtilization,
            AgeOfCreditHistory = ageOfCreditHistory
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            _service.CalculateCreditScore(customer));
    }
}