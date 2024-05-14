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
        { RotorName.I, ['R'] },
        { RotorName.II, ['F'] },
        { RotorName.III, ['W'] },
        { RotorName.IV, ['K'] },
        { RotorName.V, ['A'] },
        { RotorName.VI, ['A', 'N'] },
        { RotorName.VII, ['A', 'N'] },
        { RotorName.VIII, ['A', 'N'] },
    };
}

