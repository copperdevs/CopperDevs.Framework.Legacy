using CopperFramework.Elements.Systems;
using CopperFramework.Rendering.DearImGui;
using CopperFramework.Rendering.DearImGui.Attributes;
using CopperFramework.Rendering.DearImGui.Windows;
using CopperFramework.Util;

namespace CopperFramework.Elements.Components;

public class ParticleSystem : GameComponent
{
    private bool isActive = true;
    [Range(16, 512)] private float maxParticles = 128;
    [Range(.01f, 10)] private Vector2 lifetimeRandomRange = new(2.5f, 5f);
    [Range(.01f, 10)] private Vector2 speedRandomRange = new(1.5f, 5.25f);
    [Range(1, 128)] private Vector2 sizeRandomRange = new(16, 32);
    private List<Color> particleColors = new() { Color.White };
    private List<Particle> particles = new();

    public override void Update()
    {
        if (particles.Count < maxParticles && isActive)
            SpawnParticle();
        UpdateParticles();
        RenderParticles();
        KillParticles();

        EditorUpdate();
    }

    private void SpawnParticle()
    {
        var particle = new Particle
        {
            Lifetime = Random.Range(lifetimeRandomRange),
            Speed = Random.Range(speedRandomRange),
            Color = Random.Item(particleColors),
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
            particle.Transform.Position += MathUtil.RotationStuff(particle.Transform.Rotation) * particle.Speed;
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
        foreach (var particle in particles.ToList())
        {
            if (particle.Lifetime <= 0)
                particles.Remove(particle);
        }

        if (particles.Count == 0 && !isActive)
        {
            ComponentRegistry.CurrentComponents.Remove(this);
        }
    }

    private void EditorUpdate()
    {
        if (!DebugSystem.Instance.DebugEnabled) 
            return;
        
        Raylib.DrawCircleV(Vector2.Zero, 8, Color.Red);
    
        if (CopperImGui.AnyElementHovered) 
            return;
        
        if (Input.IsMouseButtonDown(MouseButton.Left) && ComponentBrowserWindow.CurrentObjectBrowserTarget == this)
        {
            Transform.Position = Input.MousePosition;
        }
    }
    
    [Serializable]
    public class Particle
    {
        public float Lifetime;
        public float Speed;
        public Color Color;
        public Transform Transform;
    }
}