namespace Ecommerce.Application.Common.Queries;

public abstract class PageQuery
{
    private int _page = 1;
    public int Page
    {
        get => _page;
        init => _page = value is < 1 ? 1 : value;
    }
    private int _pageSize = 20;
    public int PageSize
    {
        get => _pageSize;
        init => _pageSize = (value is < 1) ? 20 : (value > 100) ? 100 : value;
    }
}