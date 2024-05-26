namespace PontoSavi.Domain.Filters;

public class QueryResult<T>
{
    public List<T> Items { get; set; } = [];
    public int TotalCount { get; set; }

    public QueryResult() { }

    public QueryResult(List<T> items, int totalCount)
    {
        Items = items;
        TotalCount = totalCount;
    }
}
