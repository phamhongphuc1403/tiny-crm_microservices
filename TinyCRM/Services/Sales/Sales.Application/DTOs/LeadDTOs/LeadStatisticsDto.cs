namespace Sales.Application.DTOs.LeadDTOs;

public class LeadStatisticsDto
{
    public int OpenLeads { get; set; }
    public int QualifiedLeads { get; set; }
    public int DisqualifiedLeads { get; set; }
    public double AvgEstimatedRevenue { get; set; }
}