using FluentResults;

namespace Ecommerce.Application.Errors;

public class NotFoundError : Error
{
    public NotFoundError(string entity, string field, object value)
        : base($"{entity} with {field} '{value}' was not found")
    { }
}