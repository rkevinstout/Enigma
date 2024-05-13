using System.Text;

namespace Enigma;

public static class Extensions
{
    public static int GetOffset(this char a, char b) => a - b;

    public static int ToInt(this char c) => Convert.ToInt32(c - 'A');

    public static char ToChar(this int i) => Convert.ToChar(i + 65);
    
    public static Pipeline.Step CreateStep(
        this IComponent component, 
        bool isInbound = true
    ) => new(component, component.Cipher, isInbound);
    
    public static Pipeline.Step CreateStep(
        this IComponent component, 
        ICipher cipher,
        bool isInbound = true
    ) => new(component, cipher, isInbound);
    
    
    /// <summary>
    /// Swaps keys and values in a dictionary
    /// </summary>
    /// <param name="dictionary"></param>
    /// <returns>a new dictionary</returns>
    public static Dictionary<char, char> Invert(this IDictionary<char, char> dictionary) =>
        dictionary.ToDictionary(x => x.Value, x => x.Key);

    /// <summary>
    /// Rotates an array of characters
    /// </summary>
    /// <param name="input">the character array</param>
    /// <param name="offset">number of places to shift.
    /// Positive = left;
    /// Negative = Right
    /// </param>
    /// <returns>A new array of characters</returns>
    public static char[] Rotate(this char[] input, int offset)
    {
        if (offset == 0) return input;

        var abs = Math.Abs(offset) % input.Length;
        
        if (abs == 0) return input;

        var count = offset > 0 ? abs : input.Length - abs;
        
        return input
            .Skip(count)
            .Concat(input.Take(count))
            .ToArray();
    }

    public static SubstitutionCipher Rotate(this SubstitutionCipher cipher, int offset) => 
        new(cipher.Dictionary.Values.ToArray().Rotate(offset));

    public static string Encode(this ICipher cipher, string text)
    {
        var buffer = new StringBuilder();
        
        foreach (var c in text.ToCharArray())
        {
            buffer.Append(char.IsWhiteSpace(c) 
                ? c 
                : cipher.Encode(c));
        }

        return buffer.ToString();
    }
    public static string Encode(this Machine machine, string text)
    {
        var buffer = new StringBuilder();
        
        foreach (var c in text.ToCharArray())
        {
            buffer.Append(char.IsWhiteSpace(c) 
                ? c 
                : machine.Enter(c));
        }

        return buffer.ToString();
    }

    public static Dictionary<char, char> ToDictionary(this ICipher cipher) =>
        Alphabet.PlainText
            .ToCharArray()
            .ToDictionary(c => c, cipher.Encode);

    public static SubstitutionCipher ToCipher(Dictionary<char, char> dictionary) => new(dictionary);
}