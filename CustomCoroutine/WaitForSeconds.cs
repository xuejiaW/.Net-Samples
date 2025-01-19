using System.Collections;

namespace CustomCoroutine;

using System.Collections;

internal class WaitForSeconds : IEnumerator
{
    private readonly DateTime m_EndTime;

    public WaitForSeconds(float seconds) { m_EndTime = DateTime.Now + TimeSpan.FromSeconds(seconds); }

    public bool MoveNext() => DateTime.Now < m_EndTime;

    public void Reset() { }

    public object Current => null;
}