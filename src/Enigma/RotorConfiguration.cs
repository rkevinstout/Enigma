namespace Enigma;

public class RotorConfiguration
{
    public RotorName Name { get; }
    public string Alphabet { get; }
    public char Ring { get; }
    public char[] Notches { get; }


    public RotorConfiguration(
        RotorName name,
        string alphabet,
        params char[] notches
    ) : this(name, alphabet, 'A', notches)
    { }

    public RotorConfiguration(
        RotorName name, 
        string alphabet, 
        char ring,
        params char[] notches
        )
    {
        Name = name;
        Alphabet = alphabet;
        Ring = ring;
        Notches = notches;
    }
}

