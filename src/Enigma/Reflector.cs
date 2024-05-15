namespace Enigma;

public class Reflector : IComponent
{
    public string Name => _reflectorName.ToString();

    private readonly ReflectorName _reflectorName;

    private readonly SubstitutionCipher _substitutionCipher;
    public ICipher Cipher => _substitutionCipher;

    public static Reflector Create(ReflectorName name) => 
        new(name, Alphabets[name]);

    private Reflector(ReflectorName name, string alphabet)
        : this(name, new SubstitutionCipher(alphabet))
    { }
    
    private Reflector(ReflectorName name, SubstitutionCipher cipher)
    {
        _reflectorName = name;
        _substitutionCipher = cipher;
    }
    public static Dictionary<ReflectorName, string> Alphabets => new()
    {
        { ReflectorName.RefB, Alphabet.RefB },
        { ReflectorName.RefC, Alphabet.RefC },
        { ReflectorName.M4B, Alphabet.M4B },
        { ReflectorName.M4C, Alphabet.M4C },
    };
    
    
    public char Encode(char c) => _substitutionCipher.Encode(c);
    public char Decode(char c) => _substitutionCipher.Decode(c);
    
    public override string ToString() => _substitutionCipher.ToString();
}