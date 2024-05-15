namespace Enigma;

public class CharacterMap : ICipher
{
    private readonly int[] _encodings;
    private readonly Lazy<ICipher> _inversion;
    public ICipher Inversion => _inversion.Value;
    
    public CharacterMap(string characters) 
        : this(characters.ToCharArray())
    {}

    public CharacterMap(char[] characters) 
        : this(characters.Select(x => x.ToInt()).ToArray())
    { }
    
    public CharacterMap(int[] encodings)
    {
        _encodings = encodings;
        _inversion = new Lazy<ICipher>(Invert);
    }
    
    public int Encode(int i) => _encodings[i];
    
    public char Encode(char c) => Encode(c.ToInt()).ToChar();

    public int Decode(int i) => Inversion.Encode(i);
    public char Decode(char c) => Decode(c.ToInt()).ToChar();

    private CharacterMap Invert()
    {
        var output = new int[_encodings.Length];

        for (var i = 0; i < _encodings.Length; i++)
        {
            output[i] = _encodings[i];
        }
        return new CharacterMap(output);
    }
}