using FluentResults;

namespace Ecommerce.Application.Errors;

public class ConflictError : Error
{
    public ConflictError(string entity, string field, object value)
        : base($"{entity} with {field} '{value}' already exists.")
    { }
}