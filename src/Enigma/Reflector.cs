namespace Enigma;

public class Reflector : IComponent
{
    public string Name => _reflectorName.ToString();

    private readonly ReflectorName _reflectorName;

    public CharacterMap CharacterMap { get; }

    public static Reflector Create(ReflectorName name) => 
        new(name, Alphabets[name]);

    private Reflector(ReflectorName name, string alphabet)
    {
        _reflectorName = name;
        CharacterMap = new CharacterMap(alphabet);
    }
    
    public static Dictionary<ReflectorName, string> Alphabets => new()
    {
        { ReflectorName.RefA, Alphabet.RefA },
        { ReflectorName.RefB, Alphabet.RefB },
        { ReflectorName.RefC, Alphabet.RefC },
        { ReflectorName.M4B, Alphabet.M4B },
        { ReflectorName.M4C, Alphabet.M4C },
    };

    public char Encode(char c) => CharacterMap.Encode(c);
    public char Decode(char c) => CharacterMap.Encode(c);
    public override string ToString() => Name;
}