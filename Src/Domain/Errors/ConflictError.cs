using System.Net;
using FluentResults;

namespace Ecommerce.Domain.Errors;

public class ConflictError : Error
{
    public ConflictError(string entity, string field, object value)
        : base($"{entity} with {field} '{value}' already exists.")
    { }
}