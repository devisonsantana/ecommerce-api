namespace Ecommerce.Application.Common.Queries;

public class CategoryQuery : PageQuery
{
    public string? Name
    {
        get;
        init => field = value?.ToLower() ?? string.Empty;
    }
}