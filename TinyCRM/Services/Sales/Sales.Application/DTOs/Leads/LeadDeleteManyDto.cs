namespace Sales.Application.DTOs.Leads;

public class LeadDeleteManyDto
{
    public IEnumerable<Guid> Ids { get; set; } = null!;
}