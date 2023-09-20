namespace People.Application.DTOs;

public class AccountSummaryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public double TotalSales { get; set; }
}