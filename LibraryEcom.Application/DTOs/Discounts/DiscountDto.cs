namespace LibraryEcom.Application.DTOs.Discounts;

public class DiscountDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Percentage { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
}