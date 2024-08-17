using System.Diagnostics.CodeAnalysis;
using CopperDevs.Core.Utility;
using CopperDevs.DearImGui;
using CopperDevs.DearImGui.Attributes;
using CopperDevs.Framework.Rendering.DearImGui.Windows;
using Raylib_CSharp.Interact;

namespace CopperDevs.Framework.Components;

[SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global")]
[SuppressMessage("ReSharper", "ConvertToConstant.Global")]
public class ParticleSystem : GameComponent
{
    [Seperator("Settings")]
    [Range(16, 2048), Exposed] public float MaxParticles = 128;
    [Range(.01f, 10), Exposed] public Vector2 LifetimeRandomRange = new(2.5f, 5f);
    [Range(0, 2048), Exposed] public Vector2 SpeedRandomRange = new(512, 1024);
    [Range(1, 128), Exposed] public Vector2 SizeRandomRange = new(16, 32);
    [Exposed] public List<Color> ParticleColors = [Color.White];

    [Seperator]
    [Exposed] public bool IsActive = true;
    [Exposed] public bool DestroyComponentOnZeroParticles = false;
    [Exposed] public bool DestroyObjectOnZeroParticles = false;

    [Seperator("Info")]
    [Exposed] private List<Particle> particles = [];

    public override void Update()
    {
        if (particles.Count < MaxParticles && IsActive)
            SpawnParticle();
        UpdateParticles();
        RenderParticles();
        KillParticles();
    }

    private void SpawnParticle()
    {
        var particle = new Particle
        {
            Lifetime = Random.Range(LifetimeRandomRange),
            Speed = Random.Range(SpeedRandomRange),
            Color = Random.Item(ParticleColors, Color.White),
            Transform = new Transform
            {
                Position = Transform.Position,
                Scale = new Vector2(Random.Range(SizeRandomRange)),
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
        var scale = new Vector2(MathF.Abs(Transform.Scale.X), MathF.Abs(Transform.Scale.Y));
        foreach (var particle in particles)
        {
            var scale1 = particle.Transform.Scale / scale;
            var scale2 = (scale1.X + scale1.Y) / 2;

            rlGraphics.DrawCircleV((particle.Transform.Position - Transform.Position) / scale, scale2, particle.Color);
        }
    }

    private void KillParticles()
    {
        foreach (var particle in particles.ToList().Where(particle => particle.Lifetime <= 0))
        {
            particles.Remove(particle);
        }

        if (particles.Count != 0 || IsActive)
            return;

        if (DestroyComponentOnZeroParticles)
            Parent.Remove(this);

        if (!DestroyObjectOnZeroParticles)
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