namespace Enigma;

public class Reflector : IComponent
{
    public string Name => _reflectorName.ToString();

    private readonly ReflectorName _reflectorName;

    public CharacterMap CharacterMap { get; }

    public static Reflector Create(ReflectorName name) => 
        new(name, Configuration.ReflectorSettings.Data[name]);

    private Reflector(ReflectorName name, string alphabet)
    {
        _reflectorName = name;
        CharacterMap = new CharacterMap(alphabet);
    }

    public char Encode(char c) => CharacterMap.Encode(c);
    public char Decode(char c) => CharacterMap.Encode(c);
    public override string ToString() => Name;
}