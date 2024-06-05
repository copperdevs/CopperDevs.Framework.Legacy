using Box2D.NetStandard.Dynamics.World;
using CopperDevs.Framework.Elements.Components;

namespace CopperDevs.Framework.Scenes;

public class Scene : IEnumerable<GameObject>
{
    public string DisplayName { get; private set; }
    public string Id { get; private set; }

    internal List<GameObject> SceneObjects = [];

    public readonly World PhysicsWorld;

    public Scene()
    {
        Id = Guid.NewGuid().ToString();
        DisplayName = Id;

        SceneManager.RegisterScene(this);

        PhysicsWorld = new World(new Vector2(0, 9.81f));
    }

    public Scene(string displayName) : this(displayName, Guid.NewGuid().ToString())
    {
    }

    public Scene(string displayName, string id)
    {
        DisplayName = displayName;
        Id = id;

        SceneManager.RegisterScene(this);

        PhysicsWorld = new World(new Vector2(0, 9.81f));
    }

    public static implicit operator string(Scene scene) => scene.Id;

    public void Add(GameObject gameObject)
    {
        SceneObjects.Add(gameObject);

        gameObject.Scene = this;
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
        foreach (var component in SceneObjects.Select(gameObject => gameObject.GetComponent<T>(false)).OfType<T>())
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