using CopperFramework.Data;

namespace CopperFramework.Testing;

public static class Program
{
    public static void Main()
    {
        Framework.Load(() =>
        {
            var scene = new Scene("Test Scene")
            {
                new("Object One")
                {
                }
            };
        });
        Framework.Run();
    }
}