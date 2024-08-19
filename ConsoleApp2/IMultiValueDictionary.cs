namespace ServiceTitanTest;
public interface IMultiValueDictionary<K, V> : IEnumerable<KeyValuePair<K, V>>
{ 
    bool Add(K key, V value);
    void Remove(K key);
    void Clear(K key);

    IEnumerable<V> Get(K key);
    IEnumerable<V> GetOrDefault(K key);
    IEnumerable<KeyValuePair<K, V>> Flatten();        
}