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
        set => UpdatePosition(value);
    }
    public bool IsAtNotch => Ring.Notches.Contains(_position.ToChar());
    
    private SubstitutionCipher _substitutionCipher;
    public ICipher Cipher => _substitutionCipher;  

    public ICipher Shift { get; private set; }
    
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
        
        var chars = Ring.Position != 'A'
            ? wiring.Rotate((Ring.Position - 'A'))
            : wiring;
        
        _substitutionCipher = new SubstitutionCipher(chars);
        
        Shift = new CaesarSubstitutionCipher(Position * -1);
    }

    public void Advance() => Position += 1;

    private void UpdatePosition(int position)
    { 
        position %= 26;
        
        if (_position == position) return;
        
        _position = position;
        
        UpdateCipher();
    }
    
    private SubstitutionCipher CreateSubstitutionCipher(char[] chars)
    {
        var shift = (Position - Ring.Position.ToInt()).Normalize();
        
        var rotated = chars.Rotate(shift);
        
        return new SubstitutionCipher(rotated);
    }

    private void UpdateCipher()
    {
        var chars = RotorConfiguration
            .Alphabets[_rotorName]
            .ToCharArray()
            .Rotate(Ring.Position - 'A')
            .Rotate(_position);

        _substitutionCipher = new SubstitutionCipher(chars);
        
        Shift = new CaesarSubstitutionCipher(Position * -1);
    }
    
    public override string ToString() => _substitutionCipher.ToString();

    public string Dump()
    {
        return $"{_rotorName,10} {Position,3} {Cipher,25}";
    }
}