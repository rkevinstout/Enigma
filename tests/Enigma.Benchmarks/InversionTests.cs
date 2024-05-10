using BenchmarkDotNet.Attributes;

namespace Enigma.Benchmarks;

[MemoryDiagnoser]
public class InversionTests
{
    private readonly SubstitutionCipher _cipher = new(Alphabet.IV);

    [Benchmark]
    public void InvertDictionary()
    {
        var inversion = _cipher.Dictionary.Invert();
    }
}