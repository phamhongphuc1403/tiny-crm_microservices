namespace IAM.Business.Models.Users.Dto;

public class DeleteManyUsersDto
{
    public IEnumerable<Guid> Ids { get; set; } = null!;
}