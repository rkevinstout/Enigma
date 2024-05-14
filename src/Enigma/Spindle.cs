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
        Stack<Rotor> stack = new(_rotors);

        var rotor = stack.Pop();
        
        rotor.Advance();

        while (stack.Count > 0)
        {
            var next = stack.Pop();

            if (!rotor.IsAtNotch) return;
            
            next.Advance();
            rotor = next;
        }
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