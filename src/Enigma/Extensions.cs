using System.Text;

namespace Enigma;

public static class Extensions
{
    public static int GetOffset(this char a, char b) => a - b;

    public static int ToInt(this char a) => Convert.ToInt32(a - 'A');

    public static char ToChar(this int i) => Convert.ToChar(i + 65);

    public static Pipeline.Step CreateStep(
        this IComponent component, 
        Func<char, char> action,
        bool inbound = true
    ) => new(component, action, inbound);

    public static Pipeline.Step CreateStep(
        this IComponent component,
        bool inbound = true
    ) => inbound 
        ? component.CreateStep(x => component.Cipher.Encode(x), inbound) 
        : component.CreateStep(x => component.Cipher.Decode(x), inbound);

    public static Dictionary<char, char> Invert(this IDictionary<char, char> dictionary) =>
        dictionary.ToDictionary(x => x.Value, x => x.Key);

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
}