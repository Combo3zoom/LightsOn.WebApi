namespace LightsOn.Domain.Entities;

public class Customer : BaseAuditableEntity
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }

    public Customer(string name, string phoneNumber)
    {
        Name = name;
        PhoneNumber = phoneNumber;
    }

    private Customer()
    {
        Name = string.Empty;
        PhoneNumber = string.Empty;
    }
}