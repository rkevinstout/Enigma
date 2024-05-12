namespace Enigma;

public class RotorConfiguration(
    RotorName name,
    char ring,
    params char[] notches
    )
{
    public RotorName Name { get; } = name;
    public string Alphabet { get; } = RotorFactory.Alphabets[name];
    public char Ring { get; } = ring;
    public char[] Notches { get; } = notches;

    public RotorConfiguration(
        RotorName name,
        params char[] notches
    ) : this(name, 'A', notches)
    { }
}

