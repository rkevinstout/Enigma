using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;

namespace Enigma.Benchmarks;

[MemoryDiagnoser]
// [Config(typeof(Config))]
public class MachineTests
{
    private readonly Machine _machine;
    private readonly Dictionary<int, string> _dictionary;

    [Params(256, 1024, 2048, 4096)]
    public int Key;

    public MachineTests()
    {
        _machine = Build(RotorName.I, RotorName.II, RotorName.III);
        _dictionary = CreateTextDictionary();
    }

    private static Machine Build(params RotorName[] rotors)
    {
        var config = new Machine.Configuration();
        
        config.AddRotor(RotorName.I);
        config.AddRotor(RotorName.II);
        config.AddRotor(RotorName.III);

        return config.Create();
    }

    [Benchmark]
    public string Encrpyt()
    {
        var plainText = _dictionary[Key];
        
        return _machine.Encode(plainText);
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
        { 2048,  Generate(2048) },
        { 4096,  Generate(4096) },
        { 8192,  Generate(8192) }
    };

    private class Config : ManualConfig
    {
        public Config()
        {
            AddJob(new Job(Job.Dry)
            {
                // Environment = { Jit = Jit.LegacyJit, Platform = Platform.X64 },
                Run = { LaunchCount = 5, WarmupCount = 10, IterationCount = 20 }
                // Accuracy = { MaxRelativeError = 0.01 }
            });
        }
    }
}