namespace Enigma;

public class Pipeline
{
    private readonly List<IComponent> _components = [];

    public void Add(IComponent component) => _components.Add(component);
    public void Add(IEnumerable<IComponent> components) => _components.AddRange(components);

    public LinkedList<Step> Build()
    {
        if (_components.Count == 0) return [];
        
        var stack = new Stack<IComponent>(_components);

        return Build(stack);
    }

    private static LinkedList<Step> Build(Stack<IComponent> stack)
    {
        var list = new LinkedList<Step>();
        
        var component = stack.Pop();
        
        // unlike other components reflector is only called once
        list.AddFirst(component.CreateStep(x => component.Cipher.Encode(x)));

        while (stack.Count > 0)
        {
            var next = stack.Pop();
            
            if (next is Rotor { Position: > 0 } rotor)
            {
                // Add Caesar Shift to capture the offset of the rotor position
                var shift = rotor.Shift();

                list.AddFirst(new Step(rotor, shift.Encode));
                list.AddLast(new Step(rotor, shift.Decode, false)); 
            }
            // the normal encipherment of the wheels/components themselves
            list.AddFirst(next.CreateStep());
            list.AddLast(next.CreateStep(false));
        }
        return list;
    }

    /// <summary>
    /// A wrapper for a character substitution.  Mostly for logging purposes
    /// </summary>
    /// <param name="component">Plug board, reflector, rotor, etc</param>
    /// <param name="action">delegate that will perform the encipherment</param>
    /// <param name="inbound">flag to denote the monoalphabetic substitution performed</param>
    public class Step(IComponent component, Func<char, char> action, bool? inbound = true)
    {
        public IComponent Component { get; } = component;
        public Func<char, char> Action { get; } = action;
        public bool Inbound { get; } = inbound ?? true;
    }
}