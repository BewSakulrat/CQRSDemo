namespace Application.DTOs.Customer;

public class CreateCustomerDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}