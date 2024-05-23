namespace Enigma;

public class PlugBoard : IComponent
{
    public string Name => "PB";

    public CharacterMap CharacterMap { get; }
    
    private readonly Pair[] _pairs;
    
    public PlugBoard(params Pair[] pairs)
    {
        _pairs = pairs;
        var chars = Swap(pairs);

        CharacterMap = new CharacterMap(chars);
    }

    private static ReadOnlySpan<char> Swap(Pair[] pairs)
    {
        var chars = Alphabet.PlainText.ToCharArray().AsSpan();

        foreach (var pair in pairs)
        {
            chars[pair.From.ToInt()] = pair.To;
            chars[pair.To.ToInt()] = pair.From;
        }

        return chars;
    }

    public int Encode(int i) => CharacterMap.Encode(i);
    public int Decode(int i) => CharacterMap.Encode(i);
    public override string ToString() => _pairs
        .Select(x => $"{x.From}{x.To}")
        .Aggregate((a, b) => $"{a} {b}");

    public record struct Pair(char From, char To);
}