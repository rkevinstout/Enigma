namespace Enigma;

public class PlugBoard : IComponent
{
    public string Name => "PB";

    private readonly CharacterMap _characterMap;
    public ICipher Cipher => _characterMap;
    
    public PlugBoard(params Pair[] pairs)
    {
        var chars = Swap(pairs);

        _characterMap = new CharacterMap(chars);
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
    
    public char Encode(char c) => _characterMap.Encode(c);
    public char Decode(char c) => _characterMap.Decode(c);
    public override string ToString() => _characterMap.ToString();

    public record struct Pair(char From, char To);
}