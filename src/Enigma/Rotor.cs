namespace Enigma;

public class Rotor : ICipher
{
    public string Name => RotorName.ToString();
    public RotorName RotorName { get; }
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

    public ICipher Inversion => Cipher.Inversion;

    public IDictionary<char, char> Pins => Cipher.Dictionary;

    public void Advance()
    {
        Position = (Position + 1) % 26;
    }

    public char Encode(int i) => Cipher.Encode(i);
    public char Encode(char c) => Encode(c.ToInt());
    public char Decode(char c) => Decode(c.ToInt());
    public char Decode(int i) => Cipher.Decode(i);
    public override string ToString() => Cipher.ToString();
}