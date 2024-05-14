using BenchmarkDotNet.Attributes;

namespace Enigma.Benchmarks;

[MemoryDiagnoser]
public class RotorTests
{
    private readonly Rotor _rotor = Rotor.Create(RotorName.I);

    [Benchmark]
    public void Advance()
    {
        _rotor.Advance();
    }
}