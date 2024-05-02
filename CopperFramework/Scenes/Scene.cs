using CopperFramework.Elements.Components;

namespace CopperFramework.Scenes;

public class Scene : IEnumerable<GameObject>
{
    public string DisplayName { get; private set; }
    public Guid Id { get; private set; }

    internal List<GameObject> SceneObjects = new();

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

    public void Add(GameObject gameObject)
    {
        SceneObjects.Add(gameObject);

        gameObject.Scene = this;
        
        // gameObject.UpdateComponents(component => component.Start());
    }

    public void Remove(GameObject gameObject)
    {
        SceneObjects.Remove(gameObject);
        
        gameObject.UpdateComponents(component => component.Stop());
    }

    public IEnumerator<GameObject> GetEnumerator()
    {
        return SceneObjects.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}