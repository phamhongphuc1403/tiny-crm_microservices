using BuildingBlock.Domain.Model;
using BuildingBlock.Domain.Utils;
using Sales.Domain.AccountAggregate;
using Sales.Domain.DealAggregate.Enums;
using Sales.Domain.DealAggregate.Exceptions;
using Sales.Domain.LeadAggregate;

namespace Sales.Domain.DealAggregate.Entities;

public sealed class Deal : GuidEntity
{
    public Deal()
    {
    }

    internal Deal(string title, Guid customerId, Guid? leadId, string? description,
        double estimatedRevenue, double actualRevenue)
    {
        Title = title;
        CustomerId = customerId;
        LeadId = leadId;
        Description = description;
        DealStatus = DealStatus.Open;
        EstimatedRevenue = estimatedRevenue;
        ActualRevenue = actualRevenue;
        DealLines = new List<DealLine>();
    }

    internal Deal(Guid id, string title, Guid customerId, Guid? leadId, string? description,
        double estimatedRevenue, double actualRevenue)
    {
        Id = id;
        Title = title;
        CustomerId = customerId;
        LeadId = leadId;
        Description = description;
        DealStatus = DealStatus.Open;
        EstimatedRevenue = estimatedRevenue;
        ActualRevenue = actualRevenue;
        DealLines = new List<DealLine>();
    }

    public string Title { get; set; } = null!;
    public Guid CustomerId { get; set; }
    public Account Customer { get; set; } = null!;
    public Guid? LeadId { get; set; }
    public Lead? Lead { get; set; }
    public string? Description { get; set; }
    public DealStatus DealStatus { get; set; }
    public double EstimatedRevenue { get; set; }
    public double ActualRevenue { get; set; }
    public List<DealLine> DealLines { get; set; } = null!;

    internal void Update(string title, Guid customerId, Guid? leadId, string? description,
        double estimatedRevenue, double actualRevenue)
    {
        Title = title;
        CustomerId = customerId;
        LeadId = leadId;
        Description = description;
        EstimatedRevenue = estimatedRevenue;
        ActualRevenue = actualRevenue;
        UpdatedDate = DateTime.UtcNow;
    }

    internal DealLine AddDealLine(Guid productId, double price, int quantity)
    {
        var dealLine = new DealLine(productId, price, quantity);

        DealLines.Add(dealLine);

        return dealLine;
    }

    internal DealLine EditDealLine(Guid id, Guid productId, double pricePerUnit, int quantity)
    {
        var dealLine = GetDealLine(id);

        dealLine.ProductId = productId;
        dealLine.PricePerUnit = pricePerUnit;
        dealLine.Quantity = quantity;
        dealLine.TotalAmount = pricePerUnit * quantity;

        ActualRevenue += dealLine.TotalAmount;

        return dealLine;
    }

    internal void RemoveDealLine(Guid id)
    {
        var dealLine = GetDealLine(id);

        DealLines.Remove(dealLine);
    }

    internal DealLine GetDealLine(Guid dealLineId)
    {
        return Optional<DealLine>.Of(DealLines.FirstOrDefault(x => x.Id == dealLineId))
            .ThrowIfNotPresent(new DealLineNotFoundException(dealLineId)).Get();
    }

    public void RemoveDealLine(DealLine dealLine)
    {
        DealLines.Remove(dealLine);
    }
}