namespace Enigma;

public record struct Ring(char Position, char[] Notches)
{
    public static Ring Create(RotorName name, char position = 'A')
    {
        var notches = RotorFactory.Notches[name];

        return new Ring(position, notches);
    }
}
