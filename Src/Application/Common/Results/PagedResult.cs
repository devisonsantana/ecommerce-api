namespace Ecommerce.Application.Common.Results;

public class PagedResult<T>
{
    public int Page { get; private set; }
    public int PageSize { get; private set; }
    public int TotalPages { get; private set; }
    public int TotalCount { get; private set; }
    public bool HasNextPage => Page < TotalPages;
    public bool HasPreviousPage => Page > 1;
    public IEnumerable<T> Items { get; private set; }

    public PagedResult(int page, int pageSize, int totalCount, IEnumerable<T> items)
    {
        Page = page;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize); ;
        TotalCount = totalCount;
        Items = items;
    }
}