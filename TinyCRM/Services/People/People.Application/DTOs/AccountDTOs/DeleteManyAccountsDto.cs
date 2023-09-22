namespace People.Application.DTOs;

public class DeleteManyAccountsDto
{
    public IEnumerable<Guid> Ids { get; set; } = null!;
}