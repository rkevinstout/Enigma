using System.Collections.ObjectModel;

namespace Enigma;

public class Spindle
{
    private readonly Rotor[] _rotors;
    public ReadOnlyCollection<Rotor> Rotors => _rotors.AsReadOnly();

    public string Position
    {
        get => new (Rotors.Select(r => r.Position.ToChar()).ToArray());
        private init => SetRotorPositions(value);
    }

    public Spindle(params Rotor[] rotors) :this("AAAA", rotors)
    { }

    public Spindle(string position, params Rotor[] rotors)
    {
        _rotors = rotors;
        Validate();
        Position = position;
    }

    public void Advance()
    {
        Rotors.Last().Advance();
    }

    private void SetRotorPositions(string trigram)
    {
        var chars = trigram.ToCharArray();

        for (var i = 0; i < Rotors.Count; i++)
        {
            Rotors[i].Position = chars[i].ToInt();
        }
    }

    private void Validate()
    {
        var distinctRotors = Rotors.Select(x => x.Name).Distinct();

        if (distinctRotors.Count() != Rotors.Count)
        {
            throw new ArgumentException("Rotors must be unique");
        }
    }
}