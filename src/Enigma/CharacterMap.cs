namespace Enigma;

public class CharacterMap : ICipher
{
    private readonly int[] _encodings;
    
    public ReadOnlySpan<char> Encodings => _encodings
        .Select(i => i.ToChar())
        .ToArray();

    public CharacterMap Inversion => _inversion.Value;

    private readonly Lazy<CharacterMap> _inversion;
    
    public CharacterMap(string characters) 
        : this(characters.ToCharArray().AsSpan())
    { }
    
    public CharacterMap(ReadOnlySpan<char> encodings)
        : this(encodings.ToInt())
    { }
    
    public CharacterMap(ReadOnlySpan<int> encodings)
    {
        _encodings = encodings.ToArray();;
        _inversion = new Lazy<CharacterMap>(Invert);
    }
    
    public int Encode(int i) => _encodings[i];
    
    public char Encode(char c) => Encode(c.ToInt()).ToChar();

    public int Decode(int i) => Inversion.Encode(i);
    public char Decode(char c) => Decode(c.ToInt()).ToChar();

    private CharacterMap Invert()
    {
        ReadOnlySpan<int> encodings = _encodings;
        Span<int> output = new int[encodings.Length].AsSpan();

        for (var i = 0; i < encodings.Length; i++)
        {
            var value = encodings[i];
            output[value] = i;
        }
        return new CharacterMap(output);
    }
    
    public override string ToString() => new(Encodings);
}