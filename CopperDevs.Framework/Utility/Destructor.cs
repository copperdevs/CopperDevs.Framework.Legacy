using CopperDevs.Core;

namespace CopperDevs.Framework.Utility;

public static class Destructor
{
    public static Action? OnCreation;
    public static Action? OnDestruction;

    private static readonly LocalDestructor Finalise = new();

    static Destructor()
    {
        OnCreation?.Invoke();
        Console.WriteLine(nameof(OnCreation));
    }

    private sealed class LocalDestructor
    {
        ~LocalDestructor()
        {
            OnDestruction?.Invoke();
            Console.WriteLine(nameof(OnDestruction));
        }
    }
}