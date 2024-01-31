using System.Collections;
using CopperFramework.Components;
using CopperFramework.Util;
using Random = CopperFramework.Util.Random;

namespace CopperFramework.Data;

public class Scene : IEnumerable<GameObject>
{
    #region Scene Managment

    public static Dictionary<Guid, Scene> Scenes { get; internal set; } = new();
    internal static Scene TargetScene = null!;
    public static void LoadScene(Guid scene) => TargetScene = Scenes[scene];

    #endregion

    public readonly Guid SceneId;
    public readonly string SceneDisplayName;

    internal readonly List<GameObject> GameObjects = new(); 

    public static implicit operator Guid(Scene scene) => scene.SceneId;

    #region Constructors

    public Scene(Guid sceneId, string sceneDisplayName)
    {
        SceneId = sceneId;
        SceneDisplayName = sceneDisplayName;

        Scenes.Add(this, this);
        
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if(TargetScene is null)
            LoadScene(this);
    }

    public Scene(Guid sceneId) : this(sceneId, Random.RandomCharacters(Random.Range(0, 32)))
    {
    }

    public Scene(string sceneDisplayName) : this(Guid.NewGuid(), sceneDisplayName)
    {
    }

    public Scene() : this(Guid.NewGuid(), Random.RandomCharacters(Random.Range(0, 32)))
    {
    }

    #endregion


    public void Add(GameObject gameObject)
    {
        GameObjects.Add(gameObject);
    }

    public IEnumerator<GameObject> GetEnumerator()
    {
        return GameObjects.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}