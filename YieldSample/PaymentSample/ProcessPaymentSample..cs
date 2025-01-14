namespace YieldSample;

public static class ProcessPaymentSample
{
    public static void ProcessPayment()
    {
        IEnumerable<Payment> payment = GetPaymentsWithYield(10000);

        foreach (Payment p in payment)
        {
            if (p.id < 10)
                Console.WriteLine($"Payment ID: {p.id}, Name: {p.name}");
            else
                break;
        }
    }

    // The following code is equivalent to the ProcessPayment method
    public static void ProcessPaymentUsingWhile()
    {
        IEnumerable<Payment> payment = GetPayments(10000);
        using IEnumerator<Payment> paymentEnumerator = payment.GetEnumerator();
        while (paymentEnumerator.MoveNext())
        {
            Payment p = paymentEnumerator.Current;
            if (p.id < 10)
                Console.WriteLine($"Payment ID: {p.id}, Name: {p.name}");
            else
                break;
        }
    }

    private static IEnumerable<Payment> GetPayments(int count)
    {
        var payments = new List<Payment>();

        for (int i = 0; i != count; i++)
        {
            var p = new Payment();
            p.id = i;
            p.name = "Payment " + i;
            payments.Add(p);
        }

        return payments;
    }

    private static IEnumerable<Payment> GetPaymentsWithYield(int count)
    {
        for (int i = 0; i != count; i++)
        {
            var p = new Payment();
            p.id = i;
            p.name = "Payment " + i;
            yield return p;
        }
    }
}