namespace BuildingBlock.Application.DTOs;

public class FilterAndPagingResultDto<TDto>
{
    public FilterAndPagingResultDto(List<TDto> data, int skip, int take, int totalCount)
    {
        Data = data;
        Meta = new MetaDto(skip, take, totalCount);
    }

    public MetaDto Meta { get; private set; }
    public List<TDto> Data { get; private set; }
}

public class MetaDto
{
    public MetaDto(int skip, int take, int totalCount)
    {
        Skip = skip;
        Take = take;
        TotalCount = totalCount;
        TotalPages = (int)Math.Ceiling(totalCount / (double)Take);
    }

    public int Skip { get; private set; }
    public int Take { get; }
    public int TotalPages { get; private set; }
    public int TotalCount { get; private set; }
}