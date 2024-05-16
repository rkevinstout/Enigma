namespace Enigma;

public class Reflector : IComponent
{
    public string Name => _reflectorName.ToString();

    private readonly ReflectorName _reflectorName;

    private readonly CharacterMap _characterMap;
    public ICipher Cipher => _characterMap;

    public static Reflector Create(ReflectorName name) => 
        new(name, Alphabets[name]);

    private Reflector(ReflectorName name, string alphabet)
    {
        _reflectorName = name;
        _characterMap = new CharacterMap(alphabet);
    }
    
    public static Dictionary<ReflectorName, string> Alphabets => new()
    {
        { ReflectorName.RefB, Alphabet.RefB },
        { ReflectorName.RefC, Alphabet.RefC },
        { ReflectorName.M4B, Alphabet.M4B },
        { ReflectorName.M4C, Alphabet.M4C },
    };
    
    public char Encode(char c) => _characterMap.Encode(c);
    public char Decode(char c) => _characterMap.Decode(c);
    public override string ToString() => _characterMap.ToString();
}