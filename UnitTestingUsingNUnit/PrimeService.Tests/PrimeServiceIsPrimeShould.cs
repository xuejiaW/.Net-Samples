namespace PrimeService.Tests;

[TestFixture]
public class PrimeServiceIsPrimeShould
{
    private PrimeService? m_PrimService = null;

    [SetUp]
    public void SetUp() { m_PrimService = new PrimeService(); }

    [Test]
    public void IsPrime_InputIs1_ReturnFalse()
    {
        var result = m_PrimService!.IsPrime(1);
        Assert.That(result, Is.False, "1 should not be prime");
    }

    [Test]
    public void IsPrimeImpl_Is4_ReturnFalse()
    {
        var result = m_PrimService!.IsPrimeImpl(4);
        Assert.That(result, Is.False, "4 should not be prime");
    }
}