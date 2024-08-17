using CopperDevs.Framework.Components;
using nkast.Aether.Physics2D.Dynamics;

namespace CopperDevs.Framework.Scenes;

public class Scene : IEnumerable<GameObject>
{
    public string DisplayName { get; private set; }
    public string Id { get; private set; }

    internal List<GameObject> SceneObjects = [];

    internal World PhysicsWorld;

    public Scene() : this(null!, Guid.NewGuid().ToString())
    {
    }

    public Scene(string displayName) : this(displayName, Guid.NewGuid().ToString())
    {
    }

    public Scene(string displayName, string id)
    {
        if (string.IsNullOrWhiteSpace(id)) id = Guid.NewGuid().ToString();
        if (string.IsNullOrWhiteSpace(displayName)) displayName = id;

        DisplayName = displayName;
        Id = id;

        PhysicsWorld = new World(new PhysicsVector2(0.0f, 196.133f));

        SceneManager.RegisterScene(this);
    }

    public static implicit operator string(Scene scene) => scene.Id;

    public void Add(GameObject gameObject)
    {
        SceneObjects.Add(gameObject);

        gameObject.Scene = this;

        gameObject.UpdateComponents(component => component.Start());
    }

    public void Add(GameComponent component)
    {
        var gameObject = new GameObject();

        SceneObjects.Add(gameObject);

        gameObject.Scene = this;
        gameObject.GameObjectName = component.GetType().Name;

        gameObject.Add(component);

        gameObject.UpdateComponents(createdComponent => createdComponent.Start());
    }

    public void Remove(GameObject gameObject)
    {
        gameObject.UpdateComponents(component => component.Stop());
        SceneObjects.Remove(gameObject);
    }

    public IEnumerator<GameObject> GetEnumerator()
    {
        return SceneObjects.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Load(bool clone = true)
    {
        SceneManager.LoadScene(this, clone);
    }

    public T FindFirstObjectByType<T>() where T : GameComponent
    {
        foreach (var component in SceneObjects.Select(gameObject => gameObject.GetComponent<T>(false)))
            return component;

        return ComponentRegistry.Instantiate<T>(this, typeof(T).Name);
    }


    public IEnumerable<GameObject> GetAllObjectsWithComponent<T>() where T : GameComponent
    {
        return SceneObjects.Where(gameObject => gameObject.HasComponent<T>()).ToList();
    }

    public List<T> GetAllComponents<T>() where T : GameComponent
    {
        return GetAllObjectsWithComponent<T>().Select(foundObject => foundObject.GetComponent<T>()).ToList();
    }
}