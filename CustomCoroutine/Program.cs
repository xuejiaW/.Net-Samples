using CustomCoroutine;
// ReSharper disable FunctionNeverReturns

internal static class Program
{
    private static readonly List<Behaviour> s_Behaviours = [];
    private static long s_LastFrameTime = DateTime.Now.Ticks;

    private static void Main()
    {
        s_Behaviours.Add(new MySimpleBehaviour());

        foreach (Behaviour behaviour in s_Behaviours)
        {
            behaviour.Start();
        }

        while (true)
        {
            long frameTime = DateTime.Now.Ticks;
            long deltaTime = frameTime - s_LastFrameTime;
            s_LastFrameTime = frameTime;
            Time.deltaTime = (float) TimeSpan.FromTicks(deltaTime).TotalSeconds;

            foreach (Behaviour behaviour in s_Behaviours)
            {
                behaviour.Update();
                behaviour.UpdateCoroutines();
            }

            Thread.Sleep(10); // Make fps not too high
        }
    }
}