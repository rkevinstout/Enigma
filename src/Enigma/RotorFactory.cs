namespace Enigma;

public static class RotorFactory
{
    private static readonly Dictionary<RotorName, RotorConfiguration> Configurations = new()
    {
        { RotorName.I, new RotorConfiguration(RotorName.I, Alphabet.I, 'R') },
        { RotorName.II, new RotorConfiguration(RotorName.II, Alphabet.II, 'F') },
        { RotorName.III, new RotorConfiguration(RotorName.III, Alphabet.III, 'W') },
        { RotorName.IV, new RotorConfiguration(RotorName.IV, Alphabet.IV, 'K') },
        { RotorName.V, new RotorConfiguration(RotorName.V, Alphabet.V, 'A') },
        { RotorName.VI, new RotorConfiguration(RotorName.VI, Alphabet.VI, 'A', 'N') },
        { RotorName.VII, new RotorConfiguration(RotorName.VII, Alphabet.VII, 'A', 'N') },
        { RotorName.VIII, new RotorConfiguration(RotorName.VIII, Alphabet.VIII, 'A', 'N') }
    };
    
    public static readonly Dictionary<RotorName, string> Alphabets = new()
    {
        { RotorName.I, Alphabet.I},
        { RotorName.II, Alphabet.II},
        { RotorName.III, Alphabet.III},
        { RotorName.IV, Alphabet.IV},
        { RotorName.V, Alphabet.V},
        { RotorName.VI, Alphabet.VI},
        { RotorName.VII, Alphabet.VII},
        { RotorName.VIII, Alphabet.VIII}
    };

    public static Rotor Create(RotorName name) => new Rotor(Configurations[name]);
}