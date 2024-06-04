using CopperDevs.DearImGui;
using Raylib_CSharp.Interact;

namespace TopDownShooter.Components;

public class PlayerWeapon : GameComponent
{
    private static PlayerWeapon staticWeapon;

    [Exposed] private List<PlayerWeaponPreset> weaponPresets = new()
    {
        new PlayerWeaponPreset()
        {
            BulletCount = 2,
            BulletRotationSpread = 16,
            BulletPositionSpread = 8
        },
        new PlayerWeaponPreset()
        {
            BulletCount = 3,
            BulletRotationSpread = 48,
            BulletPositionSpread = 16
        },
        new PlayerWeaponPreset()
        {
            BulletCount = 4,
            BulletRotationSpread = 1,
            BulletPositionSpread = 64
        },
    };

    [Exposed] [Range(0, 2)] private int currentWeaponPresetIndex = 0;

    public class PlayerWeaponPreset
    {
        public int BulletCount = 1;
        public float BulletRotationSpread = 4;
        public float BulletPositionSpread = 4;

        public void Shoot()
        {
            for (var i = 0; i < BulletCount; i++)
                if (!CopperImGui.AnyElementHovered)
                {
                    var bullet = ComponentRegistry.Instantiate<Bullet>();
                    ref var transform = ref bullet.GetTransform();
                    transform.Position = staticWeapon.Transform.Position +
                                         new Vector2(Random.Range(-BulletPositionSpread, BulletPositionSpread), Random.Range(-BulletPositionSpread, BulletPositionSpread));
                    transform.Rotation = staticWeapon.Transform.Rotation + Random.Range(-BulletRotationSpread, BulletRotationSpread);
                    bullet.UpdateStartPosition(transform.Position);
                }
        }
    }

    public override void Start()
    {
        staticWeapon = this;
    }

    public override void Update()
    {
        currentWeaponPresetIndex = MathUtil.Clamp(currentWeaponPresetIndex, 0, weaponPresets.Count - 1);

        if (Input.IsMouseButtonDown(MouseButton.Left))
            weaponPresets[currentWeaponPresetIndex].Shoot();

        if (Input.IsKeyPressed(KeyboardKey.One))
            currentWeaponPresetIndex = 0;
        if (Input.IsKeyPressed(KeyboardKey.Two))
            currentWeaponPresetIndex = 1;
        if (Input.IsKeyPressed(KeyboardKey.Three))
            currentWeaponPresetIndex = 2;

        staticWeapon = this;
    }
}