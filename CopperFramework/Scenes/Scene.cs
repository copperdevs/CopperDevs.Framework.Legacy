using CopperFramework.Elements.Components;

namespace CopperFramework.Scenes;

public class Scene : IEnumerable<GameComponent>
{
    public string DisplayName { get; private set; }
    public Guid Id { get; private set; }

    internal List<GameComponent> SceneComponents = new();

    public Scene()
    {
        Id = Guid.NewGuid();
        DisplayName = Id.ToString();
        
        SceneManager.RegisterScene(this);
    }

    public Scene(string displayName) : this(displayName, Guid.NewGuid())
    {
    }

    public Scene(string displayName, Guid id)
    {
        DisplayName = displayName;
        Id = id;
        
        SceneManager.RegisterScene(this);
    }

    public static implicit operator Guid(Scene scene) => scene.Id;
    public static implicit operator string(Scene scene) => scene.DisplayName;

    public void Add(GameComponent gameComponent)
    {
        SceneComponents.Add(gameComponent);
        
        gameComponent.ParentScene = this;
        gameComponent.Start();
        
        gameComponent.Transform.Position = new Vector2((float)Raylib.GetScreenWidth() / 2, (float)Raylib.GetScreenHeight() / 2);
        gameComponent.Transform.Scale = 1;
        gameComponent.Transform.Rotation = 0;
    }

    public void Remove(GameComponent gameComponent)
    {
        SceneComponents.Remove(gameComponent);
        
        gameComponent.Stop();
    }

    public IEnumerator<GameComponent> GetEnumerator()
    {
        return SceneComponents.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}