using CopperDevs.Core.Utility;
using CopperDevs.DearImGui;
using CopperDevs.DearImGui.Attributes;
using CopperDevs.Framework.Rendering.DearImGui.Windows;
using Raylib_CSharp.Interact;

namespace CopperDevs.Framework.Components;

public class ParticleSystem : GameComponent
{
    [Seperator("Settings")]
    [Range(16, 2048), Exposed] public float maxParticles = 128;
    [Range(.01f, 10), Exposed] public Vector2 lifetimeRandomRange = new(2.5f, 5f);
    [Range(0, 2048), Exposed] public Vector2 speedRandomRange = new(512, 1024);
    [Range(1, 128), Exposed] public Vector2 sizeRandomRange = new(16, 32);
    [Exposed] public List<Color> particleColors = [Color.White];

    [Seperator]
    [Exposed] public bool isActive = true;
    [Exposed] public bool destroyComponentOnZeroParticles = false;
    [Exposed] public bool destroyObjectOnZeroParticles = false;

    [Seperator("Info")]
    [Exposed] private List<Particle> particles = [];

    public override void Update()
    {
        if (particles.Count < maxParticles && isActive)
            SpawnParticle();
        UpdateParticles();
        RenderParticles();
        KillParticles();
    }

    private void SpawnParticle()
    {
        var particle = new Particle
        {
            Lifetime = Random.Range(lifetimeRandomRange),
            Speed = Random.Range(speedRandomRange),
            Color = Random.Item(particleColors, Color.White),
            Transform = new Transform
            {
                Position = Transform.Position,
                Scale = Random.Range(sizeRandomRange),
                Rotation = Random.Range(360)
            },
        };
        particles.Add(particle);
    }

    private void UpdateParticles()
    {
        foreach (var particle in particles)
        {
            particle.Lifetime -= Time.DeltaTime;
            particle.Transform.Position += (MathUtil.CreateRotatedUnitVector(particle.Transform.Rotation) * particle.Speed) * Time.DeltaTime;
        }
    }

    private void RenderParticles()
    {
        var scale = MathF.Abs(Transform.Scale);
        foreach (var particle in particles)
        {
            rlGraphics.DrawCircleV((particle.Transform.Position - Transform.Position) / scale, particle.Transform.Scale / scale, particle.Color);
        }
    }

    private void KillParticles()
    {
        foreach (var particle in particles.ToList().Where(particle => particle.Lifetime <= 0))
        {
            particles.Remove(particle);
        }

        if (particles.Count != 0 || isActive)
            return;

        if (destroyComponentOnZeroParticles)
            Parent.Remove(this);

        if (!destroyObjectOnZeroParticles)
            return;

        ComponentRegistry.CurrentComponents.Remove(Parent);

        if (IsCurrentInspectionTarget())
            CopperImGui.GetWindow<ComponentsManagerWindow>()!.CurrentObjectBrowserTarget = null;
    }

    public override void DebugUpdate()
    {
        if (!Engine.Instance.GameWindowHovered)
            return;

        if (Input.IsMouseButtonDown(MouseButton.Left))
            Transform.Position = Input.MousePosition;

        rlGraphics.DrawCircleV(Transform.Position, 8, Color.Red);
    }


    [Serializable]
    public class Particle
    {
        public float Lifetime;
        public float Speed;
        public Color Color = new();
        public Transform Transform;
    }
}