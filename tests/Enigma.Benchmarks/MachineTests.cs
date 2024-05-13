using System.Text;
using BenchmarkDotNet.Attributes;

namespace Enigma.Benchmarks;

[MemoryDiagnoser]
public class MachineTests
{
    private readonly Machine _machine = Build(RotorName.I, RotorName.II, RotorName.III);
    private static readonly Dictionary<int, string> Dictionary = CreateTextDictionary();

    [Params(256, 1024)]
    public int Key;

    private static Machine Build(params RotorName[] rotors)
    {
        var config = new Machine.Configuration();
        
        config.AddRotor(RotorName.I);
        config.AddRotor(RotorName.II);
        config.AddRotor(RotorName.III);

        return config.Create();
    }

    [Benchmark]
    public void Encrpyt()
    {
        var plainText = Dictionary[Key];
        
        var cipherText = _machine.Encode(plainText);
    }
    private static string Generate(int length)
    {
        var buffer = new StringBuilder();
            
        while (length-- > 0)
        {
            var c = Random.Shared.Next(26).ToChar();
            buffer.Append(c);
        }

        return buffer.ToString();
    }

    private static Dictionary<int, string> CreateTextDictionary() => new()
    {
        { 256, Generate(256) },
        { 1024, Generate(1024) },
        { 4096,  Generate(4096) }
    };
}