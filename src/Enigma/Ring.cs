namespace Enigma;

public record struct Ring(char Position, char[] Notches)
{
    public static Ring Create(RotorName name, char position = 'A')
    {
        var notches = RotorConfiguration.Notches[name];

        // if (position != 'A')
        // {
        //     var offset = position.ToInt();
        //
        //     notches = notches
        //         .Select(notch => (notch.ToInt() - offset)
        //             .Normalize()
        //             .ToChar())
        //         .ToArray();
        // }

        return new Ring(position, notches);
    }
}
