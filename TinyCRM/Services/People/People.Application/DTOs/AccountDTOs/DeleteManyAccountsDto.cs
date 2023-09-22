namespace People.Application.DTOs.AccountDTOs;

public class DeleteManyAccountsDto
{
    public IEnumerable<Guid> Ids { get; set; } = null!;
}