namespace Enigma;

public class PlugBoard : IComponent
{
    public string Name => "PB";

    public SubstitutionCipher Cipher { get; private set; } = new();

    public PlugBoard()
    { }
    public PlugBoard(params Pair[] pairs)
    {
        Add(pairs);
    }

    public void Add(char from, char to) => Add(new Pair(from, to));

    public void Add(params Pair[] pairs)
    {
        var copy = CopyDictionary();
        
        Add(copy, pairs);

        Cipher = new SubstitutionCipher(copy);
    }

    private static void Add(IDictionary<char, char> dictionary, params Pair[] pairs)
    {
        foreach (var pair in pairs)
        {
            dictionary[pair.From] = pair.To;
            dictionary[pair.To] = pair.From;
        }
    }

    private Dictionary<char, char> CopyDictionary() => 
        Cipher.Dictionary.ToDictionary(x => x.Key, x => x.Value);

    public override string ToString() => Cipher.ToString();

    public record struct Pair(char From, char To);
}