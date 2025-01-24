using System.Collections;

namespace CustomCoroutine;

public abstract class Behaviour
{
    private readonly List<Stack<IEnumerator>> m_ActiveCoroutines = new();
    public virtual void Start() { }
    public virtual void Update() { }

    protected void StartCoroutine(IEnumerator coroutine)
    {
        m_ActiveCoroutines.Add(new Stack<IEnumerator>(new[] {coroutine}));
    }

    internal void UpdateCoroutines()
    {
        for (int index = 0; index != m_ActiveCoroutines.Count; ++index)
        {
            Stack<IEnumerator> instructions = m_ActiveCoroutines[index];
            if (instructions.Count == 0)
            {
                // Remove this coroutine
                m_ActiveCoroutines.RemoveAt(index);
                // To avoid skipping the next coroutine
                index--;
            }
            else
            {
                IEnumerator instruction = instructions.Peek();
                if (instruction.MoveNext())
                {
                    if (instruction.Current is IEnumerator nestedInstruction)
                    {
                        instructions.Push(nestedInstruction);
                    }
                }
                else
                {
                    instructions.Pop();
                }
            }
        }
    }
}