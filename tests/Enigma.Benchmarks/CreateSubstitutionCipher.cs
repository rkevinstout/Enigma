using BenchmarkDotNet.Attributes;

namespace Enigma.Benchmarks;

[MemoryDiagnoser]
public class CreateSubstitutionCipher
{
    private readonly string _alphabet = Alphabet.V;

    private readonly char[] _chars;

    public CreateSubstitutionCipher()
    {
        _chars = _alphabet.ToCharArray();
    }
    
    [Benchmark]
    public void FromString()
    {
        var cipher = new SubstitutionCipher(_alphabet);
    }

    [Benchmark]
    public void FromArray()
    {
        var cipher = new SubstitutionCipher(_chars);
    }
    
}