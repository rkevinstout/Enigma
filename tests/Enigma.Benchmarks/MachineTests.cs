using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;

namespace Enigma.Benchmarks;

[MemoryDiagnoser]
// [SimpleJob(RunStrategy.Throughput, launchCount: 10,
//    warmupCount: 10, iterationCount: 10)]
public class MachineTests
{
    private readonly Machine _machine = Build(RotorName.I, RotorName.II, RotorName.III);

    private string _text = string.Empty;

    [Params(256, 1024, 2048, 4096)]
    public int Length;
    
    [GlobalSetup]
    public void GlobalSetup()
    {
        _text = Generate(Length);
    }

    private static Machine Build(params RotorName[] rotors)
    {
        var config = new Configuration();
        
        config.Rotors.Add(rotors);

        return config.Create();
    }

    [Benchmark]
    public string Encrpyt()
    {
        return _machine.Encode(_text);
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
}