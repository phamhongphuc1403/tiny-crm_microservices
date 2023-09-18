using BuildingBlock.Domain.Model;

namespace People.Domain.Entities;

public class Account : GuidEntity
{
    public string Name { set; get; } = null!;
    public string Email { set; get; } = null!;
    public string Phone { set; get; } = null!;
    public string Address { set; get; } = null!;
    public double TotalSales { set; get; }
}