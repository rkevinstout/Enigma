using System.Collections.ObjectModel;

namespace Enigma;

public class CharacterMap : ICipher
{
    private readonly int[] _encodings;
    
    public ReadOnlyCollection<char> Encodings => _encodings
        .Select(i => i.ToChar())
        .ToArray()
        .AsReadOnly();
    
    private readonly Lazy<CharacterMap> _inversion;
    public ICipher Inversion => _inversion.Value;
    
    public CharacterMap(string characters) 
        : this(characters.ToCharArray())
    { }

    public CharacterMap(char[] characters) 
        : this(characters.Select(x => x.ToInt()).ToArray())
    { }

    public CharacterMap(ReadOnlySpan<int> encodings)
        : this(encodings.ToArray())
    { }
    
    public CharacterMap(int[] encodings)
    {
        _encodings = encodings;
        _inversion = new Lazy<CharacterMap>(Invert);
    }
    
    public int Encode(int i) => _encodings[i];
    
    public char Encode(char c) => Encode(c.ToInt()).ToChar();

    public int Decode(int i) => Inversion.Encode(i);
    public char Decode(char c) => Decode(c.ToInt()).ToChar();

    public CharacterMap Invert()
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
    
    public override string ToString() => new(_encodings.Select(i => i.ToChar()).ToArray());
}