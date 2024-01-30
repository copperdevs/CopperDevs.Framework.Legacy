namespace CopperFramework.ECS;

public class World
{
    private static List<ISystem> systems = new();

    private static List<ISystem> LoadSystems()
    {
        var targetType = typeof(ISystem);
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => targetType.IsAssignableFrom(p)).ToList();

        return types.Select(type => (ISystem)Activator.CreateInstance(type)!).ToList();
    }
    
    private List<Component> components = new();
}