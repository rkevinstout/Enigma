namespace Enigma;

public static class RotorFactory
{
    public static Dictionary<RotorName, string> Alphabets => BuildAlphabets();
    
    private static readonly Dictionary<RotorName, RotorConfiguration> Configurations = new()
    {
        { RotorName.I, new RotorConfiguration(RotorName.I, 'R') },
        { RotorName.II, new RotorConfiguration(RotorName.II, 'F') },
        { RotorName.III, new RotorConfiguration(RotorName.III, 'W') },
        { RotorName.IV, new RotorConfiguration(RotorName.IV, 'K') },
        { RotorName.V, new RotorConfiguration(RotorName.V, 'A') },
        { RotorName.VI, new RotorConfiguration(RotorName.VI, 'A', 'N') },
        { RotorName.VII, new RotorConfiguration(RotorName.VII, 'A', 'N') },
        { RotorName.VIII, new RotorConfiguration(RotorName.VIII, 'A', 'N') }
    };

    private static Dictionary<RotorName, string> BuildAlphabets() => new()
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

    public static Rotor Create(RotorName name) => new Rotor(Configurations[name]);
}