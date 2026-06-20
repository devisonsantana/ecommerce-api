namespace Ecommerce.Domain.Common;

public class BulkResult<T>(IEnumerable<T> succeeded, IEnumerable<string> failed)
{
    public IEnumerable<T> Succeeded { get; private set; } = succeeded;
    public IEnumerable<string> Failed { get; private set; } = failed;
}