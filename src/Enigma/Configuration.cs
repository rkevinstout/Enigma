namespace Enigma;

public class Configuration
{
    // TODO: add methods to set the ring settings (char and int)
    public List<Rotor> Rotors { get; } = new();
    public List<PlugBoard.Pair> Pairs { get; } = new();
    public ReflectorName ReflectorName { get; set; } = ReflectorName.RefB;
    public void AddRotor(RotorName name, int ringSetting) => 
        Rotors.Add(Rotor.Create(name, ringSetting));
    public void AddRotor(RotorName name, char ringSetting) => 
        Rotors.Add(Rotor.Create(name, ringSetting));

    public void AddRotor(RotorName name) => Rotors.Add(Rotor.Create(name));
    public void AddRotors(params RotorName[] names) => names.ToList().ForEach(AddRotor);
    public void AddPairs(params PlugBoard.Pair[] pairs) => pairs.ToList().ForEach(Pairs.Add);
        
    public void AddPair(char from, char to) => Pairs.Add(new(from, to));

    public void AddPairs(string text) =>
        text.Split(' ')
            .Select(x => new PlugBoard.Pair(x[0], x[1]))
            .ToList()
            .ForEach(Pairs.Add);

    public Machine Create() =>
        new(
            new PlugBoard(Pairs.ToArray()),
            new Spindle(Rotors.ToArray()),
            Reflector.Create(ReflectorName)
        );
}