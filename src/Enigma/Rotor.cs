namespace Enigma;
/// <summary>
/// A rotating wheel the effects a substitution cipher
/// </summary>
/// <seealso cref="https://en.wikipedia.org/wiki/Enigma_machine#Rotors"/>
public class Rotor : IComponent
{
    public string Name => RotorName.ToString();
    private RotorName RotorName { get; }
    public char Ring { get; set; }
    public SubstitutionCipher Cipher { get; private set; }        
    public IList<char> Notches { get; set; }
    
    private int _position;
    public int Position
    {
        get => _position;
        set => UpdatePosition(value);
    }

    public Rotor(RotorConfiguration config) : this(
        config.Name, 
        new SubstitutionCipher(config.Alphabet), 
        config.Ring,
        config.Notches
        )
    { }

    private Rotor(
        RotorName name, 
        SubstitutionCipher cipher, 
        char ring, 
        IList<char> notches
        )
    {
        RotorName = name;
        Cipher = cipher;
        Ring = ring;
        Notches = notches;
    }

    public void Advance() => Position += 1;

    private void UpdatePosition(int position)
    { 
        position %= 26;
        
        if (_position == position) return;
        
        _position = position;
        
        UpdateCipher();
    }

    private void UpdateCipher()
    {
        var chars = RotorFactory
            .Alphabets[RotorName]
            .ToCharArray()
            .Rotate(_position);

        Cipher = new SubstitutionCipher(chars);
    }
    
    /// <summary>
    /// Shifts the current cipher by the current position of the rotor
    /// </summary>
    /// <returns>a new cipher</returns>
    public ICipher Shift()
    {
        return new CaesarCipher(Position * -1);
    }

    public override string ToString() => Cipher.ToString();

    public string Dump()
    {
        return $"{RotorName,10} {Position,3} {Cipher,25}";
    }
}