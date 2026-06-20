using System.Net;
using FluentResults;

namespace Ecommerce.Domain.Errors;

public class NotFoundError : Error
{
    public NotFoundError(string message) : base(message)
    {
        WithMetadata(nameof(HttpStatusCode), StatusCodes.Status404NotFound);
    }
}