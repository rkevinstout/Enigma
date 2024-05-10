namespace Enigma;

public class Rotor : IComponent
{
    public string Name => RotorName.ToString();
    private RotorName RotorName { get; }
    public char Ring { get; set; }
    public SubstitutionCipher Cipher { get; }        
    public IList<char> Notches { get; set; }
    public int Position { get; set; }

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

    public void Advance()
    {
        Position = (Position + 1) % 26;
    }

    public override string ToString() => Cipher.ToString();
}