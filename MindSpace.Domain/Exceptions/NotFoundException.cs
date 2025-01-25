namespace MindSpace.Domain.Exceptions
{
    public class NotFoundException(string resourceType, string resourceIdentifier) : Exception($"{resourceType} with id: {resourceIdentifier} does not exists")
    {
    }
}
