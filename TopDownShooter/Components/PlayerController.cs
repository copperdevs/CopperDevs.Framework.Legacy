using CopperDevs.Core.Data;
using CopperDevs.Framework.Components;
using Raylib_CSharp.Collision;
using Raylib_CSharp.Interact;

namespace TopDownShooter.Components;

public class PlayerController : GameComponent
{
    public static Vector2 Pos;
    [Exposed] [Range(1, 2048)] private int moveSpeed = 1024;
    [Exposed] [ReadOnly] private Vector2 moveInput;
    [Exposed] [Range(0, 16)] private float inputSmoothTime = 12;

    public override void Start()
    {
        Transform.Scale = 24;
    }

    public override void Update()
    {
        Pos = Transform.Position;
        
        moveInput = MathUtil.Lerp(moveInput, PlayerMoveInput(), Time.DeltaTime * inputSmoothTime);

        Transform.Position += (moveInput * moveSpeed) * Time.DeltaTime;
        Transform.LookAt(Input.MousePosition);

        rlGraphics.DrawCircleV(Vector2.Zero, 1, Color.Red);

        SizeCheck(Engine.Instance.WindowSize);
    }

    public override void FixedUpdate()
    {
        EnemyManager.Instance.EnemyTargetPosition = Transform.Position;
    }

    private Vector2 PlayerMoveInput()
    {
        var value = Vector2.Normalize(
            new Vector2(
                Input.IsKeyDown(KeyboardKey.D, KeyboardKey.A),
                Input.IsKeyDown(KeyboardKey.S, KeyboardKey.W)
            ));

        if (float.IsNaN(value.X))
            value.X = 0;
        if (float.IsNaN(value.Y))
            value.Y = 0;

        return value;
    }

    public override void DebugUpdate()
    {
        var playerPos = Transform.Position;

        rlGraphics.DrawLineV(playerPos, playerPos + (moveInput * Transform.Scale), Color.Green);
        rlGraphics.DrawLineV(playerPos, playerPos + (PlayerMoveInput() * Transform.Scale), Color.Blue);

        var directionRay = new Ray(new Vector3(playerPos.X, playerPos.Y, 0), Transform.Rotation.ToRotatedUnitVector().ToVector3());
        rlGraphics.DrawRay(directionRay, Color.Red);
    }

    private void SizeCheck(Vector2Int size)
    {
        if (Transform.Position.X > size.X)
            Transform.Position.X = size.X;

        if (Transform.Position.X < 0)
            Transform.Position.X = 0;

        if (Transform.Position.Y > size.Y)
            Transform.Position.Y = size.Y;

        if (Transform.Position.Y < 0)
            Transform.Position.Y = 0;
    }
}