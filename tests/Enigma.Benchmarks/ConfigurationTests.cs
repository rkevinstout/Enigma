using BenchmarkDotNet.Attributes;

namespace Enigma.Benchmarks;

[MemoryDiagnoser]
public class ConfigurationTests
{
    private static Machine Create()
    {
        var config = new Configuration();
        
        config.Rotors.Add(RotorName.I, 'A');
        config.Rotors.Add(RotorName.II, 'B');
        config.Rotors.Add(RotorName.III, 'C');
        config.PlugBoard.Add('A', 'M');
        config.PlugBoard.Add('B', 'N');
        config.PlugBoard.Add('C', 'O');
        config.PlugBoard.Add('D', 'P');
        config.PlugBoard.Add('E', 'Q');
        config.Reflector.Name = ReflectorName.RefA;
        
        return config.Create();
    }
    
    [Benchmark]
    public Machine CreateMachine()
    {
        return Create();
    }
}