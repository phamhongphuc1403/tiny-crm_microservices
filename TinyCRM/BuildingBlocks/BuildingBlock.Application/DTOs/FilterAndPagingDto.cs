using System.ComponentModel.DataAnnotations;

namespace BuildingBlock.Application.DTOs;

public class FilterAndPagingDto<TEnum>
{
    [StringLength(100, ErrorMessage = "Keyword cannot exceed 100 characters.")]
    public string Keyword { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "PageIndex must be a non-negative number.")]
    public int PageIndex { get; set; } = 1;

    [Range(1, int.MaxValue, ErrorMessage = "PageSize must be a positive number.")]
    public int PageSize { get; set; } = int.MaxValue;

    public bool IsDescending { get; set; } = true;

    public virtual TEnum? SortBy { get; set; }

    public string ConvertSort()
    {
        if (SortBy == null) return string.Empty;

        var sort = SortBy.ToString();

        sort = IsDescending ? $"{sort} asc" : $"{sort} desc";

        return sort;
    }
}