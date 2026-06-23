namespace Ecommerce.Application.Common.Results;

public class BulkResult<T>(IEnumerable<T> Succeeded, IEnumerable<string> Failed)
{
    public IEnumerable<T> Succeeded { get; private set; } = Succeeded;
    public IEnumerable<string> Failed { get; private set; } = Failed;
}