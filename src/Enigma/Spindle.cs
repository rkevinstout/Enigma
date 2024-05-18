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
        get => GetRotorPositions;
        set => SetRotorPositions(value);
    }

    private Rotor Left => Rotors[2];
    private Rotor Middle => Rotors[1];
    private Rotor Right => Rotors[0];
    
    public string Rings => string.Join("", Rotors.Reverse().Select(r => r.Ring.Position));

    public Spindle(params Rotor[] rotors)
    {
        _rotors = rotors.Reverse().ToArray();
        Validate();
    }

    public void Advance()
    {
        // 4th rotor (if present) does not rotate
        //var rotors = Rotors.Take(3).ToArray();

        if (Middle.IsAtNotch)
        {
            Middle.Advance();
            Left.Advance();
        }
        else if (Right.IsAtNotch)
        {
            Middle.Advance();
        }
        Right.Advance();
    }
    
    private string GetRotorPositions => new (Rotors
        .Reverse()
        .Select(r => r.Position.ToChar())
        .ToArray()
    );

    private void SetRotorPositions(string trigram)
    {
        var chars = trigram.Reverse().ToArray();

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
    
    public override string ToString() => string.Join("-", Rotors.Reverse().Select(r => r.Name));
}