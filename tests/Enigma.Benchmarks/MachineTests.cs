using BenchmarkDotNet.Attributes;

namespace Enigma.Benchmarks;

[MemoryDiagnoser]
public class MachineTests
{
    private readonly Machine _machine = Build(RotorName.I, RotorName.II, RotorName.III);

    private const string LoremIpsum =
        """
        Lorem ipsum dolor sit amet consectetur adipiscing elit sed do eiusmod tempor incididunt ut labore et dolore magna aliqua"
        """;
    
    private readonly string _text = LoremIpsum.ToUpper();

    private static Machine Build(params RotorName[] rotors)
    {
        var config = new Machine.Configuration();
        
        config.AddRotor(RotorName.I);
        config.AddRotor(RotorName.II);
        config.AddRotor(RotorName.III);

        return config.Create();
    }

    [Benchmark]
    public void Enter()
    {
        var cipherText = _machine.Encode(_text);
    }
}