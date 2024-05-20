namespace Enigma;

public readonly struct CharacterMap
{
    private readonly int[] _encodings;
    
    public ReadOnlySpan<char> Encodings => _encodings
        .Select(i => i.ToChar())
        .ToArray();
    
    public CharacterMap() 
        : this(Alphabet.PlainText) 
    { }
    
    public CharacterMap(string characters) 
        : this(characters.AsSpan())
    { }
    
    public CharacterMap(ReadOnlySpan<char> encodings)
        : this(encodings.ToInt())
    { }

    private CharacterMap(ReadOnlySpan<int> encodings) => 
        _encodings = encodings.ToArray();

    public int Encode(int i) => _encodings[i];
    public char Encode(char c) => Encode(c.ToInt()).ToChar();

    internal CharacterMap Invert()
    {
        ReadOnlySpan<int> encodings = _encodings;
        Span<int> output = new int[encodings.Length].AsSpan();

        for (var i = 0; i < encodings.Length; i++)
        {
            var index = encodings[i];
            output[index] = i;
        }
        return new CharacterMap(output);
    }
    
    public override string ToString() => new(Encodings);
}