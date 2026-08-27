namespace Ecommerce.Application.Extensions;

public static class CollectionExtensions
{
    public static PagedResult<T> ToPagedList<T>(this IEnumerable<T> source, int page, int pageSize) where T : class
    {
        var items = source.ToList();
        var totalCount = items.Count;
        var pagedItems = items.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return new PagedResult<T>
        {
            Items = pagedItems,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }

    public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T>? source)
    {
        return source ?? Enumerable.Empty<T>();
    }

    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? source)
    {
        return source is null || !source.Any();
    }

    public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector) where TKey : notnull
    {
        var seen = new HashSet<TKey>();
        return source.Where(item => seen.Add(keySelector(item)));
    }

    public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source)
    {
        return source.Select((item, index) => (item, index));
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        var random = new Random();
        return source.OrderBy(_ => random.Next());
    }

    public static IEnumerable<T> Page<T>(this IEnumerable<T> source, int page, int pageSize) where T : class
    {
        return source.Skip((page - 1) * pageSize).Take(pageSize);
    }

    public static IEnumerable<(T First, T Second)> Pairwise<T>(this IEnumerable<T> source)
    {
        using var enumerator = source.GetEnumerator();
        while (enumerator.MoveNext())
        {
            var first = enumerator.Current;
            if (!enumerator.MoveNext()) break;
            var second = enumerator.Current;
            yield return (first, second);
        }
    }

    public static IReadOnlyList<IReadOnlyList<T>> Chunk<T>(this IEnumerable<T> source, int size)
    {
        var result = new List<IReadOnlyList<T>>();
        var chunk = new List<T>();
        foreach (var item in source)
        {
            chunk.Add(item);
            if (chunk.Count == size)
            {
                result.Add(chunk.AsReadOnly());
                chunk = new List<T>();
            }
        }
        if (chunk.Count > 0)
            result.Add(chunk.AsReadOnly());
        return result.AsReadOnly();
    }
}

public class PagedResult<T> where T : class
{
    public List<T> Items { get; set; } = [];
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < TotalPages;
}
