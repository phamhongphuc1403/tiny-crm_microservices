using BuildingBlock.Domain.Model;
using Sales.Domain.AccountAggregate;
using Sales.Domain.DealAggregate.Enums;
using Sales.Domain.DealAggregate.Exceptions;
using Sales.Domain.LeadAggregate;

namespace Sales.Domain.DealAggregate;

public class Deal : GuidEntity
{
    internal Deal(string title, Guid customerId, Guid? leadId, string? description,
        decimal estimatedRevenue)
    {
        Title = title;
        CustomerId = customerId;
        LeadId = leadId;
        Description = description;
        DealStatus = DealStatus.Open;
        EstimatedRevenue = estimatedRevenue;
        DealLines = new List<DealLine>();
    }

    public string Title { get; set; }
    public Guid CustomerId { get; set; }
    public Account Customer { get; set; } = null!;
    public Guid? LeadId { get; set; }
    public Lead? Lead { get; set; }
    public string? Description { get; set; }
    public DealStatus DealStatus { get; set; }
    public decimal EstimatedRevenue { get; set; }
    public decimal ActualRevenue { get; set; }

    public List<DealLine> DealLines { get; set; }

    internal void Update(string title, Guid customerId, Guid? leadId, string? description, DealStatus dealStatus,
        decimal estimatedRevenue, decimal actualRevenue)
    {
        Title = title;
        CustomerId = customerId;
        LeadId = leadId;
        Description = description;
        DealStatus = dealStatus;
        EstimatedRevenue = estimatedRevenue;
        ActualRevenue = actualRevenue;
    }

    internal void AddDealLine(Guid productId, string code, double price, int quantity, double totalAmount)
    {
        DealLines.Add(new DealLine(Guid.NewGuid(), productId, code, price, quantity, totalAmount));
    }

    internal void UpdateDealLine(Guid id, Guid productId, string code, double price, int quantity, double totalAmount)
    {
        var dealLine = DealLines.FirstOrDefault(x => x.Id == id);
        if (dealLine == null) throw new DealLineNotFoundException(id);

        dealLine.ProductId = productId;
        dealLine.Code = code;
        dealLine.Price = price;
        dealLine.Quantity = quantity;
        dealLine.TotalAmount = totalAmount;
    }

    internal void RemoveDealLine(Guid id)
    {
        var dealLine = DealLines.FirstOrDefault(x => x.Id == id);
        if (dealLine == null) throw new DealLineNotFoundException(id);

        DealLines.Remove(dealLine);
    }
}