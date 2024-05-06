using CopperDearImGui;

namespace TopDownShooter.Components;

public class PlayerWeapon : GameComponent
{
    private static PlayerWeapon weapon;
    
    [Exposed] private List<PlayerWeaponPreset> weaponPresets = new()
    {
        new PlayerWeaponPreset(),
        new PlayerWeaponPreset(),
        new PlayerWeaponPreset()
    };

    [Exposed] private int currentWeaponPresetIndex = 0;

    public class PlayerWeaponPreset
    {
        [Exposed] private int bulletCount = 1;
        [Exposed] private float bulletSpread = 4;
        [Exposed] private List<Bullet> bullets = new();

        public void Shoot()
        {
            for (var i = 0; i < bulletCount; i++)
                if (!CopperImGui.AnyElementHovered)
                    bullets.Add(new Bullet());
        }

        public void Render()
        {
            foreach (var bullet in bullets)
            {
                bullet.Render();   
            }
        }

        public class Bullet
        {
            public Vector2 Position;
            public Vector2 Direction;

            public void Render()
            {
                Raylib.DrawCircleV((-weapon.Transform.Position) + Position, 6, Color.RayWhite);
            }
        }
    }

    public override void Start()
    {
        weapon = this;
    }

    public override void Update()
    {
        currentWeaponPresetIndex = MathUtil.Clamp(currentWeaponPresetIndex, 0, weaponPresets.Count - 1);

        if (Input.IsMouseButtonDown(MouseButton.Left))
            weaponPresets[currentWeaponPresetIndex].Shoot();

        foreach (var weapon in weaponPresets) 
            weapon.Render();
    }
}