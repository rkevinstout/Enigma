using System.Text;
using BenchmarkDotNet.Attributes;

namespace Enigma.Benchmarks;

[MemoryDiagnoser]
public class MachineTests
{
    private Machine _machine = null!;
    private string _text = string.Empty;

    [Params(256, 1024, 4096)]
    public int Length;
    
    [GlobalSetup]
    public void GlobalSetup()
    {
        _text = Generate(Length);
        _machine = Build(RotorName.I, RotorName.II, RotorName.III);
    }

    [Benchmark]
    public string Encrpyt()
    {
        return _machine.Encode(_text);
    }
    
    private static string Generate(int length)
    {
        var buffer = new StringBuilder(length);
            
        while (length-- > 0)
        {
            var c = Random.Shared.Next(26).ToChar();
            buffer.Append(c);
        }
        return buffer.ToString();
    }

    private static Machine Build(params RotorName[] rotors)
    {
        var config = new Configuration();
        
        config.Rotors.Add(rotors);

        return config.Create();
    }
}