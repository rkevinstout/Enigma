namespace Enigma;

using RotorSetting = (RotorName Name, char Ring);

public class Configuration
{
    public RotorSettings Rotors { get; } = new();
    public PlugBoardSettings PlugBoard { get; } = new();
    public ReflectorName Reflector { get; set; } = ReflectorName.RefB;

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
        Enigma.Reflector.Create(Reflector)
    );
}