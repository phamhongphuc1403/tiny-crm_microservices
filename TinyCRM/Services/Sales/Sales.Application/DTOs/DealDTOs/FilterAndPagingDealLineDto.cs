using BuildingBlock.Application.DTOs;
using Sales.Application.DTOs.DealDTOs.Enums;

namespace Sales.Application.DTOs.DealDTOs;

public class FilterAndPagingDealLineDto : FilterAndPagingDto<DealLineSortProperty>
{
    public override string ConvertSort()
    {
        var sort = SortBy switch
        {
            DealLineSortProperty.ProductCode => "Product.Code",
            DealLineSortProperty.ProductName => "Product.Name",
            _ => SortBy.ToString()
        };

        sort = IsDescending ? $"{sort} desc" : $"{sort} asc";

        return sort;
    }
}