using BuildingBlock.Domain.Model;

namespace People.Domain.Entities;

public class Account : GuidEntity
{
    private Account(string name, string email, string phone, string address, double totalSales)
    {
        Name = name;
        Email = email;
        Phone = phone;
        Address = address;
        TotalSales = totalSales;
    }

    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public string Address { get; private set; }
    public double TotalSales { get; private set; }
}