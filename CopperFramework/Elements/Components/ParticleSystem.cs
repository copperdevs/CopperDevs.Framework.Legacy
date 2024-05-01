using CopperCore.Utility;
using CopperDearImGui.Attributes;
using CopperFramework.Rendering.DearImGui.Windows;

namespace CopperFramework.Elements.Components;

public class ParticleSystem : GameComponent
{
    [Seperator("Settings")] 
    [Range(16, 512), Exposed] private float maxParticles = 128;
    [Range(.01f, 10), Exposed] private Vector2 lifetimeRandomRange = new(2.5f, 5f);
    [Range(.01f, 10), Exposed] private Vector2 speedRandomRange = new(1.5f, 5.25f);
    [Range(1, 128), Exposed] private Vector2 sizeRandomRange = new(16, 32);
    [Exposed] private List<Color> particleColors = new() { Color.White };

    [Seperator] 
    [Exposed] private bool isActive = true;
    [Exposed] private bool destroyComponentOnZeroParticles = false;
    [Exposed] private bool destroyObjectOnZeroParticles = false;

    [Seperator("Info")] [Exposed] private List<Particle> particles = new();

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
            particle.Transform.Position +=
                MathUtil.CreateRotatedUnitVector(particle.Transform.Rotation) * particle.Speed;
        }
    }

    private void RenderParticles()
    {
        foreach (var particle in particles)
        {
            Raylib.DrawCircleV(particle.Transform.Position - Transform.Position, particle.Transform.Scale,
                particle.Color);
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

        if (ComponentsManagerWindow.CurrentObjectBrowserTarget == Parent)
            ComponentsManagerWindow.CurrentObjectBrowserTarget = null;
    }

    public override void DebugUpdate()
    {
        Raylib.DrawCircleV(Vector2.Zero, 8, Color.Red);
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