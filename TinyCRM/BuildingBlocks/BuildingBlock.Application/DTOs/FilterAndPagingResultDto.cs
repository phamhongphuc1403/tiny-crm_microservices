namespace BuildingBlock.Application.DTOs;

public class FilterAndPagingResultDto<TDto>
{
    public FilterAndPagingResultDto(List<TDto> data, int totalCount)
    {
        Data = data;
        TotalCount = totalCount;
    }

    public int TotalCount { get; private set; }
    public List<TDto> Data { get; private set; }
}