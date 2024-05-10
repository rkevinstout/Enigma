using BenchmarkDotNet.Attributes;

namespace Enigma.Benchmarks;

[MemoryDiagnoser]
public class RotationTests
{
    private readonly char[] _chars = Alphabet.I.ToCharArray();

    [Params(1, 0, -1)]
    public int Offset;
    
    [Benchmark]
    public void Rotate()
    {
        var rotated = _chars.Rotate(Offset);
    }

}