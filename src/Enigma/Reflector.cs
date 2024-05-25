namespace Enigma;

public class Reflector : IComponent
{
    public string Name => _reflectorName.ToString();

    private readonly ReflectorName _reflectorName;

    public CharacterMap CharacterMap { get; }
    public CharacterMap InvertedMap => CharacterMap;

    public static Reflector Create(ReflectorName name) => 
        new(name, Configuration.ReflectorSettings.Data[name]);

    private Reflector(ReflectorName name, string alphabet)
    {
        _reflectorName = name;
        CharacterMap = new CharacterMap(alphabet);
    }
    
    public int Encode(int i) => CharacterMap.Encode(i);
    public int Decode(int i) => CharacterMap.Encode(i);
    public override string ToString() => Name;
}