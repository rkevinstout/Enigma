namespace Enigma;

public record struct RotorDescription(
    RotorName Name, 
    string Wiring, 
    params char[] Notches
    )
{
    public static Dictionary<RotorName, RotorDescription> Data => new()
    {
        { RotorName.I, new(RotorName.I, Alphabet.I, 'Q') },
        { RotorName.II, new (RotorName.II,Alphabet.II, 'E') },
        { RotorName.III, new (RotorName.III, Alphabet.III, 'V') },
        { RotorName.IV, new (RotorName.IV, Alphabet.IV, 'J') },
        { RotorName.V, new (RotorName.V, Alphabet.V, 'Z') },
        { RotorName.VI, new (RotorName.VI, Alphabet.VI, 'Z', 'M') },
        { RotorName.VII, new (RotorName.VII, Alphabet.VII, 'Z', 'M') },    
        { RotorName.VIII, new (RotorName.VIII, Alphabet.VIII, 'Z', 'M') },
    };

}


