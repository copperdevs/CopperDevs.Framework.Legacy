namespace TopDownShooter.Components;

public class PlayerWeapon : GameComponent
{
    // [Seperator("Weapons")] 
    // [Range(1, 256)] private int primaryBulletCount = 4;
    // [Range(1, 256)] private int secondaryBulletCount = 32;
    // private bool bulletParticleTrailEnabled = true;
    // private bool requireAmmo = true;

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
    }

    public override void Update()
    {
        currentWeaponPresetIndex = MathUtil.Clamp(currentWeaponPresetIndex, 0, weaponPresets.Count - 1);
    }
}