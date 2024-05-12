namespace Enigma;

public class Reflector : IComponent
{
    public string Name => _reflectorName.ToString();

    private readonly ReflectorName _reflectorName;
    public SubstitutionCipher Cipher { get; }

    public static Reflector Create(ReflectorName name) => 
        new(name, ReflectorFactory.Alphabets[name]);

    private Reflector(ReflectorName name, string alphabet)
        : this(name, new SubstitutionCipher(alphabet))
    { }
    
    private Reflector(ReflectorName name, SubstitutionCipher cipher)
    {
        _reflectorName = name;
        Cipher = cipher;
    }
    
    public override string ToString() => Cipher.ToString();
}