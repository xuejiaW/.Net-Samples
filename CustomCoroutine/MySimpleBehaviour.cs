using System.Collections;

namespace CustomCoroutine;

public class MySimpleBehaviour : Behaviour
{
    public override void Start()
    {
        base.Start();
        
        StartCoroutine(Foo());
        StartCoroutine(Bar());
    }

    public override void Update() { Console.Title = $"Running at {1 / Time.deltaTime:F0} FPS"; }

    private IEnumerator Foo()
    {
        yield return Foo2();
        Console.WriteLine("Foo Finished!");
    }

    private IEnumerator Foo2()
    {
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine(i);
            yield return new WaitForSeconds(1);
        }

        Console.WriteLine("Foo2 Finished!");
    }

    private IEnumerator Bar()
    {
        yield return new WaitForSeconds(2);
        Console.WriteLine("Bar Finished!");
    }
}