namespace PrimeService;

public class PrimeService
{
    public bool IsPrime(int candidate)
    {
        if (candidate < 2)
        {
            return false;
        }

        return IsPrimeImpl(candidate);
    }

    internal bool IsPrimeImpl(int candidate)
    {
        for (int i = 2; i < candidate; i++)
        {
            if (candidate % i == 0)
                return false;
        }

        return true;
    }
}