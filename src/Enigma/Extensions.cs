namespace Enigma;

public static class Extensions
{
    public static int GetOffset(this char a, char b)
    {
        return a - b;
    }

    public static int ToInt(this char a)
    {
        return Convert.ToInt32(a - 'A');
    }

    public static char ToChar(this int i)
    {
        return Convert.ToChar(i + 65);
    }

    public static Pipeline.Step CreateStep(
        this IComponent component, 
        Func<char, char> action,
        bool? inbound = true
        ) => new(component, action, inbound);
}