using BenchmarkDotNet.Attributes;

namespace Enigma.Benchmarks;

[MemoryDiagnoser]
public class CaesarShiftTests
{
    private readonly ICipher _dictionary = new CaesarSubstitutionCipher(15);
    private readonly ICipher _algo = new CaesarCipher(15);
    
    private const string LoremIpsum = "Lorem ipsum dolor sit amet consectetur adipiscing elit sed do eiusmod tempor incididunt ut labore et dolore magna aliqua";
    
    private readonly string _text = LoremIpsum.ToUpper();

    [Benchmark(Baseline = true)]
    public void EncodeDictionary()
    {
        //var cipher = new CaesarCipher(15);
        
        var result = _dictionary.Encode(_text);
    }

    [Benchmark]
    public void EncodeAlgo()
    {
        //var cipher  = new AlgoCaesarCipher(15);
        var result = _algo.Encode(_text);
    }
}