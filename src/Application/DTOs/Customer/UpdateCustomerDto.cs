namespace Application.DTOs.Customer;

public class UpdateCustomerDto
{
    public Guid Id { get; set; } // Id for identifying the customer to update
    public string? Name { get; set; }
    public string? Email { get; set; }
}