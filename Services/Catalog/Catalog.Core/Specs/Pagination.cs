namespace Catalog.Core.Specs;

public class Pagination<T> where T: class
{
    public IReadOnlyList<T> Data { get; set; }
    public int Count { get; set; }
    public int PageSize { get; set; }
    public int PageIndex { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Pagination()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        
    }
    public Pagination(int pageIndex, int pageSize, int count, IReadOnlyList<T> data)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        Count = count;
        Data = data;
    }
}