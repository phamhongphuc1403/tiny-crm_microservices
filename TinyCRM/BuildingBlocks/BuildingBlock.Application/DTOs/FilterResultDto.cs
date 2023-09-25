namespace BuildingBlock.Application.DTOs;

public class FilterResultDto<TDto>
{
    public FilterResultDto(List<TDto> data)
    {
        Data = data;
    }

    public List<TDto> Data { get; private set; }
}