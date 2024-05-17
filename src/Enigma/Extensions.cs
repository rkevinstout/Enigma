using System.Text;

namespace Enigma;

public static class Extensions
{
    public static int ToInt(this char c) => Convert.ToInt32(c - 'A');

    public static char ToChar(this int i) => Convert.ToChar(i + 65);
    
    public static Pipeline.Step CreateStep(
        this IComponent component, 
        bool isInbound = true
    ) => new(component, isInbound);
    
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
        var count = Normalize(offset, input.Length);

        if (count == 0) return input;
        
        return input
            .Skip(count)
            .Concat(input.Take(count))
            .ToArray();
    }

    public static int Normalize(this int input, int @base = 26)
    {
        if (input == 0) return input;

        var abs = Math.Abs(input) % @base;

        return input >= 0 ? abs : @base - abs;
    }

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
    
    public static CharacterMap Invert(this CharacterMap map)
    {
        var output = new char[map.Encodings.Count];

        for (var i = 0; i < map.Encodings.Count; i++)
        {
            var value = map.Encodings[i].ToInt();
            output[value] = i.ToChar();
        }
        return new CharacterMap(output);
    }
}