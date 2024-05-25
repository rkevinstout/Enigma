namespace Enigma;

using RotorSetting = (RotorName Name, char Ring);

public class Configuration
{
    public RotorSettings Rotors { get; } = new();
    public PlugBoardSettings PlugBoard { get; } = new();
    public ReflectorSettings Reflector { get; } = new();

    public class ReflectorSettings
    {
        public ReflectorName Name { get; set; } = ReflectorName.RefB;
        
        public Reflector Create() => Enigma.Reflector.Create(Name);
        
        public static Dictionary<ReflectorName, string> Data { get; } = new()
        {
            { ReflectorName.RefA, Alphabet.RefA },
            { ReflectorName.RefB, Alphabet.RefB },
            { ReflectorName.RefC, Alphabet.RefC },
            { ReflectorName.M4B, Alphabet.M4B },
            { ReflectorName.M4C, Alphabet.M4C },
        };
    }

    public class RotorSettings
    {
        private readonly List<RotorSetting> _list = [];
        
        public void Add(params RotorName[] names) => 
            names.ToList().ForEach(x => Add(x));
        
        public void Add(RotorName name, char ringSetting = 'A') => 
            _list.Add((name, ringSetting));
        public void Add(RotorName name, int ringSetting) => 
            _list.Add((name, (ringSetting - 1).ToChar()));

        /// <summary>
        /// Sets the rings for each rotor
        /// </summary>
        /// <remarks>unlike the internal implementation, this method assumes one-based
        /// indexing to conform to informal usage</remarks>
        /// <param name="rings"></param>
        public void SetRings(params int[] rings) =>
            SetRings(rings.Select(x => (x - 1).ToChar()).ToArray());
        public void SetRings(params char[] rings)
        {
            for (var i = 0; i < rings.Length; i++)
            {
                _list[i] = (_list[i].Name, rings[i]);
            }
        }
        private Rotor[] ToArray() => 
            _list.Select(x => Rotor.Create(x.Name, x.Ring)).ToArray();
        
        public Spindle CreateSpindle() => new(ToArray());

        public record struct RotorDescription(
            RotorName Name,
            string Wiring,
            params char[] Notches
        );
        
        public static Dictionary<RotorName, RotorDescription> Data { get; } = new()
        {
            { RotorName.I, new(RotorName.I, Alphabet.I, 'Q') },
            { RotorName.II, new(RotorName.II, Alphabet.II, 'E') },
            { RotorName.III, new(RotorName.III, Alphabet.III, 'V') },
            { RotorName.IV, new(RotorName.IV, Alphabet.IV, 'J') },
            { RotorName.V, new(RotorName.V, Alphabet.V, 'Z') },
            { RotorName.VI, new(RotorName.VI, Alphabet.VI, 'Z', 'M') },
            { RotorName.VII, new(RotorName.VII, Alphabet.VII, 'Z', 'M') },
            { RotorName.VIII, new(RotorName.VIII, Alphabet.VIII, 'Z', 'M') },
        };
    }
    
    public class PlugBoardSettings
    {
        private readonly List<PlugBoard.Pair> _pairs = [];
        
        public void Add(char from, char to) => _pairs.Add(new(from, to));
        public void Add(params PlugBoard.Pair[] pairs) => 
            pairs.ToList().ForEach(_pairs.Add);
        public void Add(string text) =>
            text.Split(' ')
                .Select(x => new PlugBoard.Pair(x[0], x[1]))
                .ToList()
                .ForEach(_pairs.Add);
        public PlugBoard CreatePlugBoard() => new(_pairs.ToArray());
    }

    public Machine Create() => new(
        PlugBoard.CreatePlugBoard(),
        Rotors.CreateSpindle(),
        Reflector.Create()
    );
}