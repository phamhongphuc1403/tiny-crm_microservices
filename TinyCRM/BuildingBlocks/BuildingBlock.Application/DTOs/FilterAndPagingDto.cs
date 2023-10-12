using System.ComponentModel.DataAnnotations;

namespace BuildingBlock.Application.DTOs;

public class FilterAndPagingDto<TEnum>
{
    [StringLength(100, ErrorMessage = "Keyword cannot exceed 100 characters.")]
    public string Keyword { get; set; } = string.Empty;

    [Range(0, int.MaxValue, ErrorMessage = "PageIndex must be a non-negative number.")]
    public int Skip { get; set; } = 0;

    [Range(1, int.MaxValue, ErrorMessage = "PageSize must be a positive number.")]
    public int Take { get; set; } = 10;

    public bool IsDescending { get; set; }

    public virtual TEnum? SortBy { get; set; }

    public virtual string ConvertSort()
    {
        if (SortBy == null) return string.Empty;

        var sort = SortBy.ToString();

        sort = IsDescending ? $"{sort} desc" : $"{sort} asc";

        return sort;
    }
}