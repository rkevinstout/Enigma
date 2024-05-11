using BenchmarkDotNet.Attributes;

namespace Enigma.Benchmarks;

[MemoryDiagnoser]
public class RotorTests
{
    private readonly Rotor _rotor = RotorFactory.Create(RotorName.I);

    [Benchmark]
    public void Advance()
    {
        _rotor.Advance();
    }
}