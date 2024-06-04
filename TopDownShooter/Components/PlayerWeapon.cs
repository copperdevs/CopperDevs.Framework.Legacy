using CopperDevs.DearImGui;
using Raylib_CSharp.Interact;

namespace TopDownShooter.Components;

public class PlayerWeapon : GameComponent
{
    private static PlayerWeapon staticWeapon;

    [Exposed] private PlayerWeaponSettings primaryWeapon = new()
    {
        BulletCount = 3,
        BulletRotationSpread = 16,
        BulletPositionSpread = 8,
        ShootDelay = 0.015f
    };

    [Exposed] private PlayerWeaponSettings secondaryWeapon = new()
    {
        BulletCount = 128,
        BulletRotationSpread = 64,
        BulletPositionSpread = 16,
        ShootDelay = 0.5f
    };

    [Exposed] private PlayerWeaponSettings specialWeapon = new()
    {
        BulletCount = 256,
        BulletRotationSpread = 360,
        BulletPositionSpread = 32,
        ShootDelay = 1
    };
    
    

    public class PlayerWeaponSettings
    {
        public int BulletCount = 1;
        public float BulletRotationSpread = 4;
        public float BulletPositionSpread = 4;
        public float ShootDelay = 0.15f;

        [ReadOnly] private bool canShoot = true;

        public void Shoot()
        {
            if (!canShoot || CopperImGui.AnyElementHovered)
                return;

            canShoot = false;

            for (var i = 0; i < BulletCount; i++)
            {
                var bullet = ComponentRegistry.Instantiate<Bullet>();
                ref var transform = ref bullet.GetTransform();
                transform.Position = staticWeapon.Transform.Position + new Vector2(
                    Random.Range(-BulletPositionSpread, BulletPositionSpread),
                    Random.Range(-BulletPositionSpread, BulletPositionSpread));
                transform.Rotation = staticWeapon.Transform.Rotation + Random.Range(-BulletRotationSpread, BulletRotationSpread);
                bullet.UpdateStartPosition(transform.Position);
            }

            Time.Invoke(ResetShoot, ShootDelay);
        }

        private void ResetShoot()
        {
            canShoot = true;
        }
    }

    public override void Start()
    {
        staticWeapon = this;
    }

    public override void Update()
    {
        if (Input.IsMouseButtonDown(MouseButton.Left) && Input.IsMouseButtonDown(MouseButton.Right))
        {
            specialWeapon.Shoot();
            return;
        }
        
        if (Input.IsMouseButtonDown(MouseButton.Left))
            primaryWeapon.Shoot();

        if (Input.IsMouseButtonPressed(MouseButton.Right))
            secondaryWeapon.Shoot();

        staticWeapon = this;
    }
}