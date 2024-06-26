namespace Enigma;

using RotorDescription = Configuration.RotorSettings.RotorDescription;
/// <summary>
/// A rotating wheel the effects a substitution cipher
/// </summary>
/// <seealso href="https://en.wikipedia.org/wiki/Enigma_machine#Rotors"/>
public class Rotor : IComponent
{
    public string Name => _rotorName.ToString();
    private readonly RotorName _rotorName;
    public int RingPosition { get; }

    private char[] Notches { get; }

    private int _position;
    public int Position
    {
        get => _position;
        set => _position = value.Normalize();
    }

    private int Offset => (Position - RingPosition).Normalize();
    public bool IsAtNotch => Notches.Contains(Position.ToChar());
    
    public CharacterMap CharacterMap { get; }

    public  CharacterMap InvertedMap { get; }
    
    public static Rotor Create(RotorName name) => Create(name, 'A');
    public static Rotor Create(RotorName name, int ring) =>
        new(Configuration.RotorSettings.Data[name], ring);
    public static Rotor Create(RotorName name, char ring) =>
        new(Configuration.RotorSettings.Data[name], ring.ToInt());
    
    private Rotor(RotorDescription description, int ring = 0) : this(
        description.Name, 
        description.Wiring.AsSpan(),
        ring,
        description.Notches
        )
    { }
    
    private Rotor(RotorName name, ReadOnlySpan<char> wiring, int ring, params char[] notches)
    {
        _rotorName = name;
        CharacterMap = new CharacterMap(wiring);
        InvertedMap = CharacterMap.Invert();
        RingPosition = ring;
        Notches = notches;
        Position = 0;
    }

    public void Advance() => Position += 1;

    public int Encode(int i) => Encode(i, CharacterMap);
    public int Decode(int i) => Encode(i, InvertedMap);

    private int Encode(int i, CharacterMap map)
    {
        var key = (Offset + i).Normalize();
        
        var result = (map.Encode(key) - Offset).Normalize();

        return result;
    }
    
    public override string ToString() => CharacterMap.ToString();
}