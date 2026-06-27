namespace Ecommerce.Application.Common.Queries;

public class BrandQuery : PageQuery
{
    public string? Name
    {
        get;
        init => field = value?.ToLower() ?? string.Empty;
    }
}