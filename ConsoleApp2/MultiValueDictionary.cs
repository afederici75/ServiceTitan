using System.Collections;

namespace ServiceTitanTest;

public class MultiValueDictionary<K, V> : IMultiValueDictionary<K, V>
    where K : notnull
{
    readonly Dictionary<K, HashSet<V>> _items = [];

    public bool Add(K key, V value)
    {
        if (!_items.TryGetValue(key, out var hit))
        {
            hit = [];
            _items.Add(key, hit);
        }

        return hit.Add(value);
    }

    public void Remove(K key)
    {
        if (!_items.Remove(key))
        {
            throw new KeyNotFoundException();
        }
    }

    public void Clear(K key)
    {
        if (!_items.TryGetValue(key, out var items))
        {
            throw new KeyNotFoundException();
        }
        
        items.Clear();
    }

    public IEnumerable<V> Get(K key)
    {
        if (_items.TryGetValue(key, out var result))
        {
            return result;
        }

        return Enumerable.Empty<V>();
    }

    public IEnumerable<V> GetOrDefault(K key)
    {
        if (_items.TryGetValue(key, out var list))
            return list;

        return Enumerable.Empty<V>();
    }

    public IEnumerable<KeyValuePair<K, V>> Flatten()
    {
        return _items.SelectMany(kvp => kvp.Value.Select(v => new KeyValuePair<K, V>(kvp.Key, v)));
    }

    public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
    {
        return Flatten().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {        
        return Flatten().GetEnumerator();
    }
}
