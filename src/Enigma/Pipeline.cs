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

        list.AddFirst(component.CreateStep(x => component.Cipher.Encode(x)));

        while (stack.Count > 0)
        {
            var next = stack.Pop();


            var pre = list.AddBefore(list.First!, next.CreateStep());
            
            if (next is Rotor inRotor)
            {
                var shift = inRotor.Shift();
                var step = new Step(inRotor, shift.Encode);
                list.AddAfter(pre, step);
            }
            
            var post =list.AddAfter(list.Last!, next.CreateStep(false));
            
            if (next is Rotor outRotor)
            {
                var shift = outRotor.Shift();
                var step = new Step(outRotor, shift.Decode, false);
                list.AddBefore(post, step);
            }
        }
        
        
        return list;
    }

    public class Step(IComponent component, Func<char, char> action, bool? inbound = true)
    {
        public IComponent Component { get; } = component;
        public Func<char, char> Action { get; } = action;
        public bool Inbound { get; } = inbound ?? true;
    }
}