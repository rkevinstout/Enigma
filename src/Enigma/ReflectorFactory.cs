namespace Enigma;

public static class ReflectorFactory
{
    public static Dictionary<ReflectorName, string> Alphabets => BuildAlphabets();
    private static Dictionary<ReflectorName, string> BuildAlphabets() => new()
    {
        { ReflectorName.RefB, Alphabet.RefB },
        { ReflectorName.RefC, Alphabet.RefC },
        { ReflectorName.M4B, Alphabet.M4B },
        { ReflectorName.M4C, Alphabet.M4C },
    };
}