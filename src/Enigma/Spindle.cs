using System.Collections.ObjectModel;

namespace Enigma;
/// <summary>
/// A collection of rotors
/// </summary>
public class Spindle
{
    private readonly Rotor[] _rotors;
    public ReadOnlyCollection<Rotor> Rotors => _rotors.AsReadOnly();

    public string Position
    {
        get => new (Rotors.Select(r => r.Position.ToChar()).ToArray());
        set => SetRotorPositions(value);
    }

    public Spindle(params RotorName[] rotors) 
        : this(rotors.Select(Rotor.Create).ToArray())
    { }

    public Spindle(params Rotor[] rotors)
    {
        _rotors = rotors;
        Validate();
    }

    public void Advance()
    {
        const int left = 2;
        const int middle = 1;
        const int right = 0;
        
        var rotors = Rotors.Take(3).Reverse().ToArray();

        if (rotors[middle].IsAtNotch)
        {
            rotors[middle].Advance();
            rotors[left].Advance();
        }
        else if (rotors[right].IsAtNotch)
        {
            rotors[middle].Advance();
        }
        rotors[right].Advance();
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
        var distinct = Rotors
            .Select(x => x.Name)
            .Distinct()
            .Count();

        if (distinct != Rotors.Count)
        {
            throw new ArgumentException("Rotors must be unique");
        }
    }
}