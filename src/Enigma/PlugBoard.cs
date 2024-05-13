namespace Enigma;

public class PlugBoard : IComponent
{
    public string Name => "PB";

    private readonly SubstitutionCipher _substitutionCipher;
    public ICipher Cipher => _substitutionCipher;
    
    public PlugBoard(params Pair[] pairs)
    {
        var dictionary = Alphabet.PlainText.ToCharArray()
            .Select((c, index) => new KeyValuePair<char, char>(index.ToChar(), c))
            .ToDictionary();

        Add(dictionary, pairs);

        _substitutionCipher = new SubstitutionCipher(dictionary);
    }
    
    private static void Add(IDictionary<char, char> dictionary, params Pair[] pairs)
    {
        foreach (var pair in pairs)
        {
            dictionary[pair.From] = pair.To;
            dictionary[pair.To] = pair.From;
        }
    }

    public override string ToString() => _substitutionCipher.ToString();

    public record struct Pair(char From, char To);
}