namespace Enigma;

public record struct Ring(char Position, char[] Notches)
{
    public static Ring Create(RotorName name) => Create(name, 'A');
    public static Ring Create(RotorName name, int position) => Create(name, (position -1).ToChar());
    public static Ring Create(RotorName name, char position)
    {
        var notches = RotorConfiguration.Notches[name];

        return new Ring(position, notches);
    }
}