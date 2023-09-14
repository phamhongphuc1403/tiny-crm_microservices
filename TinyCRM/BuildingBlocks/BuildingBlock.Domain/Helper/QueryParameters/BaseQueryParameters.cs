namespace BuildingBlock.Domain.Helper.QueryParameters;

public abstract class BaseQueryParameters
{
    public string? KeyWord { get; set; }
    public string? IncludeTables { get; set; }
    public string? Sorting { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}