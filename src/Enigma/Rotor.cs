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
    
    public ICipher Cipher => _characterMap;  
    
    private readonly CharacterMap _characterMap;

    public static Rotor Create(RotorName name) => new(RotorConfiguration.Create(name));
    public static Rotor Create(RotorName name, char ringSetting) =>
        new(RotorConfiguration.Create(name, ringSetting));

    private Rotor(RotorConfiguration config) : this(
        config.Name, 
        config.Wiring.ToCharArray(), 
        config.Ring
        )
    { }

    private Rotor(
        RotorName name, 
        char[] wiring,
        Ring ring
        )
    {
        _rotorName = name;
        Ring = ring;
        Position = 0;
        _characterMap = new CharacterMap(wiring);
    }

    public void Advance() => Position += 1;
    
    public char Encode(char c) => Encode(c.ToInt()).ToChar();

    private int Encode(int i)
    {
        var key = (Offset + i).Normalize();
        
        var result = (_characterMap.Encode(key) - Offset).Normalize();

        return result;
    }
        
    public char Decode(char c) => Decode(c.ToInt()).ToChar();
    
    private int Decode(int i)
    {
        var key = (Offset + i).Normalize();
        
        var result = (_characterMap.Inversion.Encode(key) - Offset).Normalize();

        return result;
    }
    
    public override string ToString() => _characterMap.ToString();

    public string Dump()
    {
        return $"{_rotorName,10} {Position,3} {Cipher,25}";
    }
}