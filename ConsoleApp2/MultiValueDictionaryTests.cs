namespace ServiceTitanTest;

public static class MultiValueDictionaryTests
{
    public static void RunAllTests()
    {
        // MultiValueDictionary tests
        EnsureTestDictionaryIsCorrect();
        AddItems();
        RemoveItems();
        ClearItems();
        ClearItemsWithWrongKey();
        FlattenItems();
        GetEnumeratorItems();
        IEnumerableGetEnumeratorItems();
        
        // Extensions tests
        Union();
        Intersection();
        Difference();
        SymmetricDifference();
    }

    static void EnsureTestDictionaryIsCorrect()
    {
        var mvd = CreateTestDictionary();
        
        Assert(mvd.Count() == TotalCount, "Wrong total count");
        Assert(mvd.Get(Animals).Count() == AnimalsCount, "Wrong animals count");
        Assert(mvd.Get(Birds).Count() == BirdsCount, "Wrong birds count");

        Assert(mvd.GetOrDefault("non-existing").Count() == 0, "Non-existing key should return empty collection");
    }

    static void AddItems()
    {
        var mvd = CreateTestDictionary();

        Assert(mvd.Count()==TotalCount, "Wrong count");
    }

    static void RemoveItems()
    {
        var mvd = CreateTestDictionary();

        mvd.Remove(Animals);

        Assert(mvd.Count() == TotalCount - AnimalsCount, "Wrong count after remove");
        Assert(mvd.Get(Animals).Count() == 0, "Animals should be empty after remove");
    }

    static void ClearItems()
    {
        var mvd = CreateTestDictionary();

        mvd.Clear(Animals);

        Assert(mvd.Count() == TotalCount - AnimalsCount, "Wrong count after clear");
        Assert(mvd.Get(Animals).Count() == 0, "Animals should be empty after clear");
    }

    static void ClearItemsWithWrongKey()
    {
        var mvd = CreateTestDictionary();

        mvd.Clear(Animals);

        Assert(mvd.Count() == TotalCount - AnimalsCount, "Wrong count after clear");
        Assert(mvd.Get(Animals).Count() == 0, "Animals should be empty after clear");
    }

    static void FlattenItems()
    {
        var mvd = CreateTestDictionary();

        var flattened = mvd.Flatten();

        Assert(flattened.Count() == TotalCount, "Wrong count after flatten");
    }

    static void GetEnumeratorItems()
    {
        var mvd = CreateTestDictionary();

        var enumerator = mvd.GetEnumerator();

        Assert(enumerator.MoveNext(), "MoveNext should return true");
        Assert(enumerator.Current.Key == Animals, "Wrong key");
        Assert(enumerator.Current.Value == "cat", "Wrong value");
    }

    static void IEnumerableGetEnumeratorItems()
    {
        var mvd = CreateTestDictionary();

        var enumerator = ((IEnumerable<KeyValuePair<string, string>>)mvd).GetEnumerator();

        Assert(enumerator.MoveNext(), "MoveNext should return true");
        Assert(enumerator.Current.Key == Animals, "Wrong key");
        Assert(enumerator.Current.Value == "cat", "Wrong value");
    }

    // Union tests

    static void Union()
    {
        // Setup
        var a = MultiValueDictionaryTests.CreateTestDictionary();
        a.Remove(Animals);

        var b = MultiValueDictionaryTests.CreateTestDictionary();
        b.Remove(Birds);

        // Test
        var results = a.Union(b);

        // Assert
        Assert(results.Count() == MultiValueDictionaryTests.TotalCount, "Wrong count after union");
    }

    static void Intersection()
    {
        // Setup
        var a = MultiValueDictionaryTests.CreateTestDictionary();
        a.Remove(Birds);
        a.Add(Cars, "bmw");

        var b = MultiValueDictionaryTests.CreateTestDictionary();
        b.Remove(Animals);
        b.Add(Cars, "bmw");

        // Test 
        var resultset = a.Intersect(b);

        // Assert
        Assert(resultset.Count() == 1, "Should be 1 (cars/bmw)");
    }

    static void Difference()
    {
        // Setup
        var a = MultiValueDictionaryTests.CreateTestDictionary();
        a.Add(Cars, "bmw");

        var b = MultiValueDictionaryTests.CreateTestDictionary();
        b.Remove(Animals);

        // Test
        var results = a.Difference(b);

        // Assert
        Assert(results.Count() == MultiValueDictionaryTests.AnimalsCount + 1, "Should be the animals count plus one car");
    }

    static void SymmetricDifference()
    {
        // Setup
        var a = MultiValueDictionaryTests.CreateTestDictionary();
        a.Add(Cars, "bmw");

        var b = MultiValueDictionaryTests.CreateTestDictionary();
        b.Remove(Birds);
        b.Add(Cars, "bmw");

        // Test
        var resultset = a.SymmetricDifference(b); 

        // Assert
        Assert(resultset.Count() == MultiValueDictionaryTests.BirdsCount, "Should be the birds count");
    }

    // Utility stuff

    static void Assert(bool condition, string? message = default)
    { 
        if (!condition)
            throw new Exception(message ?? "Test failed");
    }

    const string Animals = "animals";
    const string Birds = "birds";
    const string Cars = "cars";

    const int AnimalsCount = 3;
    const int BirdsCount = 2;
    const int TotalCount = AnimalsCount + BirdsCount;
    static MultiValueDictionary<string, string> CreateTestDictionary()
    {
        var dictionary = new MultiValueDictionary<string, string>
        {
            { Animals, "cat" },
            { Animals, "dog" },
            { Animals, "horse" },
            { Birds, "eagle" },
            { Birds, "hawk" }
        };

        return dictionary;
    }
}