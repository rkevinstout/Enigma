namespace Enigma;
/// <summary>
/// A rotating wheel the effects a substitution cipher
/// </summary>
/// <seealso cref="https://en.wikipedia.org/wiki/Enigma_machine#Rotors"/>
public class Rotor : IComponent
{
    public string Name => _rotorName.ToString();
    private readonly RotorName _rotorName;
    public Ring Ring { get; }

    private int _position;
    public int Position
    {
        get => _position;
        set => _position = value % 26;
    }

    private int Offset => (Position - Ring.Position.ToInt()).Normalize();
    public bool IsAtNotch => Ring.Notches.Contains(Position.ToChar());
    
    public CharacterMap CharacterMap { get; }

    private readonly CharacterMap _inversion;
    
    public static Rotor Create(RotorName name) => new(RotorConfiguration.Create(name));
    public static Rotor Create(RotorName name, int ringSetting) =>
        new(RotorConfiguration.Create(name, ringSetting));
    public static Rotor Create(RotorName name, char ringSetting) =>
        new(RotorConfiguration.Create(name, ringSetting));

    private Rotor(RotorConfiguration config) : this(
        config.Name, 
        config.Wiring.AsSpan(),
        config.Ring
        )
    { }

    private Rotor(
        RotorName name, 
        ReadOnlySpan<char> wiring,
        Ring ring
        )
    {
        _rotorName = name;
        Ring = ring;
        Position = 0;
        CharacterMap = new CharacterMap(wiring);
        _inversion = CharacterMap.Inversion;
    }

    public void Advance() => Position += 1;
    
    public char Encode(char c) => Encode(c.ToInt()).ToChar();

    private int Encode(int i) => Encode(i, CharacterMap);

    private int Encode(int i, CharacterMap map)
    {
        var key = (Offset + i).Normalize();
        
        var result = (map.Encode(key) - Offset).Normalize();

        return result;
    }
        
    public char Decode(char c) => Decode(c.ToInt()).ToChar();
    
    private int Decode(int i) => Encode(i, _inversion);
    
    public override string ToString() => CharacterMap.ToString();
}