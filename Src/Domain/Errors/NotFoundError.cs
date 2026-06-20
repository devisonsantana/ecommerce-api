using System.Net;
using FluentResults;

namespace Ecommerce.Domain.Errors;

public class NotFoundError : Error
{
    public NotFoundError(string entity, string field, object value)
        : base($"{entity} with {field} '{value}' was not found")
    { }
}