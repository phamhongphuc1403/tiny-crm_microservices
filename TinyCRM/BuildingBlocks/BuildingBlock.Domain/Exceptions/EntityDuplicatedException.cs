namespace BuildingBlock.Domain.Exceptions;

public class EntityDuplicatedException : Exception
{
    protected EntityDuplicatedException(string entity, string column, object value) : base(
        $"{entity} with {column}: {value} is already existed")
    {
    }
}