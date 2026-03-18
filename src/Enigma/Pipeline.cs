namespace Enigma;

public class Pipeline
{
    private readonly List<IComponent> _components = [];
    public void Add(IComponent component) => _components.Add(component);
    public void Add(IEnumerable<IComponent> components) => _components.AddRange(components);

    public Step[] Build()
    {
        if (_components.Count == 0) return [];

        var stack = new Stack<IComponent>(_components);

        return Build(stack);
    }

    private static Step[] Build(Stack<IComponent> stack)
    {
        var list = new List<Step>();

        var component = stack.Pop();

        // unlike other components reflector is only called once
        list.Add(component.CreateStep());

        while (stack.Count > 0)
        {
            var next = stack.Pop();

            // the normal encipherment of the wheels/components themselves
            list.Insert(0, next.CreateStep());
            list.Add(next.CreateStep(false));
        }
        return list.ToArray();
    }

    /// <summary>
    /// A wrapper for a character substitution.  Mostly for logging purposes
    /// </summary>
    public class Step
    {
        public IComponent Component { get; }
        public bool Inbound { get; }
        public int Execute(int input) => Inbound
            ? Component.Encode(input)
            : Component.Decode(input);

        /// <summary>
        /// A wrapper for a character substitution.  Mostly for logging purposes
        /// </summary>
        /// <param name="component">Plug board, reflector, rotor, etc</param>
        /// <param name="isInbound">flag to denote the monoalphabetic substitution performed</param>
        public Step(IComponent component, bool isInbound = true)
        {
            Component = component;
            Inbound = isInbound;
        }
    }
}