using System.Net;
using FluentResults;

namespace Ecommerce.Domain.Errors;

public class ConflictError : Error
{
    public ConflictError(string message) : base(message)
    {
        WithMetadata(nameof(HttpStatusCode), StatusCodes.Status409Conflict);
    }
}