namespace Enigma;

public record RotorConfiguration(
    RotorName Name,
    Ring Ring
    )
{
    public static RotorConfiguration Create(RotorName name, char ringSetting = 'A') =>
        new(name, Ring.Create(name, ringSetting));
    
    public string Alphabet { get; } = RotorFactory.Alphabets[Name];
}

