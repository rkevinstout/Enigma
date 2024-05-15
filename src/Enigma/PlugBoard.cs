namespace Enigma;

public class PlugBoard : IComponent
{
    public string Name => "PB";

    private readonly SubstitutionCipher _substitutionCipher;
    public ICipher Cipher => _substitutionCipher;
    
    public PlugBoard(params Pair[] pairs)
    {
        var chars = Swap(pairs);

        _substitutionCipher = new SubstitutionCipher(chars);
    }

    private static char[] Swap(Pair[] pairs)
    {
        var chars = Alphabet.PlainText.ToCharArray();

        foreach (var pair in pairs)
        {
            chars[pair.From.ToInt()] = pair.To;
            chars[pair.To.ToInt()] = pair.From;
        }

        return chars;
    }
    public override string ToString() => _substitutionCipher.ToString();

    public record struct Pair(char From, char To);
}