namespace ServiceTitanTest;

public static class MultiValueDictionaryExtensions
{
    public static IMultiValueDictionary<K, V> Union<K, V>(this IMultiValueDictionary<K, V> instance, IMultiValueDictionary<K, V> other)
        where K : notnull
    {
        FlattenBoth(instance, other, out var a, out var b);

        var results = a.Union(b);

        return CreateDictionary(results);
    }

    public static IMultiValueDictionary<K, V> Intersect<K, V>(this IMultiValueDictionary<K, V> instance, IMultiValueDictionary<K, V> other)
        where K : notnull
    {
        FlattenBoth(instance, other, out var a, out var b);

        var results = a.Intersect(b);

        return CreateDictionary(results);
    }

    public static IMultiValueDictionary<K, V> Difference<K, V>(this IMultiValueDictionary<K, V> instance, IMultiValueDictionary<K, V> other)
        where K : notnull
    {
        FlattenBoth(instance, other, out var a, out var b);

        var acceptedItems = a.Where(itemInA => !b.Contains(itemInA));
        
        return CreateDictionary(acceptedItems);
    }

    public static IMultiValueDictionary<K, V> SymmetricDifference<K, V>(this IMultiValueDictionary<K, V> instance, IMultiValueDictionary<K, V> other)
        where K : notnull
    {
        FlattenBoth(instance, other, out var a, out var b);

        var intersection = a.Intersect(b);

        var fromA = a.Where(x => !intersection.Contains(x));
        var fromB = b.Where(x => !intersection.Contains(x));

        var results = fromA.Union(fromB);

        return CreateDictionary(results);
    }

    // Internals
    static IMultiValueDictionary<K, V> CreateDictionary<K, V>(IEnumerable<KeyValuePair<K, V>> keyValuePairs)
        where K : notnull
    {
        var result = new MultiValueDictionary<K, V>();
        foreach (var item in keyValuePairs)
        {
            result.Add(item.Key, item.Value);
        }

        return result;
    }

    static void FlattenBoth<K, V>(IMultiValueDictionary<K, V> instance, IMultiValueDictionary<K, V> other, out IEnumerable<KeyValuePair<K, V>> a, out IEnumerable<KeyValuePair<K, V>> b) where K : notnull
    {
        a = instance.Flatten();
        b = other.Flatten();
    }
}