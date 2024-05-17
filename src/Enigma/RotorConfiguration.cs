namespace Enigma;

public record RotorConfiguration(
    RotorName Name,
    Ring Ring
    )
{
    public static RotorConfiguration Create(RotorName name, char ringSetting = 'A') =>
        new(name, Ring.Create(name, ringSetting));

    public string Wiring => Alphabets[Name];
    
    public static Dictionary<RotorName, string> Alphabets => new()
    {
        { RotorName.I, Alphabet.I },
        { RotorName.II, Alphabet.II },
        { RotorName.III, Alphabet.III },
        { RotorName.IV, Alphabet.IV },
        { RotorName.V, Alphabet.V },
        { RotorName.VI, Alphabet.VI },
        { RotorName.VII, Alphabet.VII },
        { RotorName.VIII, Alphabet.VIII }
    };

    public static Dictionary<RotorName, char[]> Notches => new()
    {
        { RotorName.I, ['Q'] },
        { RotorName.II, ['E'] },
        { RotorName.III, ['V'] },
        { RotorName.IV, ['J'] },
        { RotorName.V, ['Z'] },
        { RotorName.VI, ['Z', 'M'] },
        { RotorName.VII, ['Z', 'M'] },
        { RotorName.VIII, ['Z', 'M'] },
    };
}

