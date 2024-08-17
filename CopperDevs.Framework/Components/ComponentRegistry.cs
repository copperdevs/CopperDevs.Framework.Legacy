using CopperDevs.Core;
using CopperDevs.Framework.Attributes;
using CopperDevs.Framework.Scenes;

namespace CopperDevs.Framework.Components;

public static class ComponentRegistry
{
    internal static List<GameObject> CurrentComponents
    {
        get => SceneManager.ActiveScene.SceneObjects;
        set => SceneManager.ActiveScene.SceneObjects = value;
    }

    internal static List<Type> ComponentTypes { get; private set; } = AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(assembly => assembly.GetTypes())
        .Where(type => type.IsSubclassOf(typeof(GameComponent)))
        .Where(type => !Attribute.IsDefined(type, typeof(DisabledAttribute))).ToList();

    internal static Dictionary<Type, List<Type>> AbstractChildren = new();

    public static void RegisterAbstractSubclass<TBase, TSub>() where TBase : GameComponent where TSub : GameComponent
    {
        if (AbstractChildren.ContainsKey(typeof(TBase)))
            AbstractChildren[typeof(TBase)].Add(typeof(TSub));
        else
            AbstractChildren.Add(typeof(TBase), [typeof(TSub)]);

        Log.Info($"Registering a new subclass of type `{typeof(TSub).FullName}` for abstract class of type `{typeof(TBase).FullName}`");
    }

    public static T Instantiate<T>() where T : GameComponent
    {
        return (T)Instantiate(typeof(T), SceneManager.ActiveScene, $"New {typeof(T).Name}");
    }

    public static GameComponent Instantiate(Type type)
    {
        return Instantiate(type, SceneManager.ActiveScene, $"New {type.Name}");
    }

    public static T Instantiate<T>(Scene targetScene) where T : GameComponent
    {
        return (T)Instantiate(typeof(T), targetScene, $"New {typeof(T).Name}");
    }

    public static T Instantiate<T>(string name) where T : GameComponent
    {
        return (T)Instantiate(typeof(T), SceneManager.ActiveScene, name);
    }

    public static GameComponent Instantiate(Type type, string name)
    {
        return Instantiate(type, SceneManager.ActiveScene, name);
    }

    public static T Instantiate<T>(Scene targetScene, string name) where T : GameComponent
    {
        return (T)Instantiate(typeof(T), targetScene, name);
    }

    public static T Instantiate<T>(GameObject targetGameObject) where T : GameComponent
    {
        return (T)Instantiate(typeof(T), targetGameObject);
    }

    public static GameComponent Instantiate(Type type, GameObject targetObject)
    {
        var targetType = type;

        if (AbstractChildren.TryGetValue(type, out var childTypes))
            targetType = Random.Item(childTypes);

        var createdComponent = Activator.CreateInstance(targetType) as GameComponent ?? null!;
        targetObject.Add(createdComponent);

        return createdComponent;
    }

    public static GameComponent Instantiate(Type type, Scene targetScene, string name)
    {
        var targetType = type;

        if (AbstractChildren.TryGetValue(type, out var childTypes))
            targetType = Random.Item(childTypes);

        var createdComponent = Activator.CreateInstance(targetType) as GameComponent ?? null!;
        targetScene.Add(new GameObject(name) { createdComponent });
        return createdComponent;
    }
}