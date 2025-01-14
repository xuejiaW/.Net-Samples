namespace YieldSample;

public static class YieldOrderSample
{
    public static void Output()
    {
        var numbers = ProduceEvenNumbers(5);
        Console.WriteLine("Caller: about to iterate.");
        foreach (int i in numbers)
        {
            Console.WriteLine($"Caller: {i}");
        }
    }

    private static IEnumerable<int> ProduceEvenNumbers(int upto)
    {
        Console.WriteLine("Iterator: start.");
        for (int i = 0; i <= upto; i += 2)
        {
            Console.WriteLine($"Iterator: about to yield {i}");
            yield return i;
            Console.WriteLine($"Iterator: yielded {i}");
        }

        Console.WriteLine("Iterator: end.");
    }
}