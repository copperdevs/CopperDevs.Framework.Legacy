namespace CopperFramework.ECS;

public interface ISystem
{
    public void ModifyComponents<TQuery>(List<Component> components) where TQuery : Query;
}