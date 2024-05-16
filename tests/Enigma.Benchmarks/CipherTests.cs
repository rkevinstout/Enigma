using BenchmarkDotNet.Attributes;

namespace Enigma.Benchmarks;

[MemoryDiagnoser]
public class CipherTests
{
    private const string LoremIpsum = "LOREM IPSUM DOLOR SIT AMET CONSECTETUR ADIPISCING ELIT SED DO EIUSMOD TEMPOR INCIDIDUNT UT LABORE ET DOLORE MAGNA ALIQUA";

    [Params(true, false)]
    public bool Create { get; set; }

    readonly ICipher _substitution = new SubstitutionCipher(Alphabet.IV);
    readonly ICipher _charMap = new CharacterMap(Alphabet.IV);
    
    
    [Benchmark(Baseline = true)]
    public string SubstitutionCipher()
    {
        var cipher = Create ? new SubstitutionCipher(Alphabet.IV) : _substitution;

        return cipher.Encode(LoremIpsum);
    }
    
    [Benchmark]
    public string CharacterMap()
    {
        var cipher = Create ? new CharacterMap(Alphabet.IV) : _charMap;

        return cipher.Encode(LoremIpsum);
    }
}