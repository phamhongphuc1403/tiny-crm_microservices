namespace Sales.Application.DTOs.ProductDTOs;

public class DeleteManyProductsDto
{
    public IEnumerable<Guid> Ids { get; set; } = null!;
}