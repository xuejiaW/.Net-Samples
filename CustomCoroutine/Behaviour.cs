using System.Collections;

namespace CustomCoroutine;

public abstract class Behaviour
{
    private readonly List<IEnumerator> m_ActiveCoroutines = new();
    public virtual void Start() { }
    public virtual void Update() { }

    protected void StartCoroutine(IEnumerator coroutine) { m_ActiveCoroutines.Add(coroutine); }

    internal void UpdateCoroutines()
    {
        for (int i = m_ActiveCoroutines.Count - 1; i >= 0; i--)
        {
            IEnumerator coroutine = m_ActiveCoroutines[i];
            if (!coroutine.MoveNext())
            {
                m_ActiveCoroutines.RemoveAt(i);
            }
        }
    }
}