namespace Enigma;

public static class ReflectorFactory
{
    public static Dictionary<ReflectorName, string> Alphabets => BuildAlphabets();
    private static Dictionary<ReflectorName, string> BuildAlphabets() => new()
    {
        { ReflectorName.RefB, Alphabet.RefB },
        { ReflectorName.RefC, Alphabet.RefC }
    };
}