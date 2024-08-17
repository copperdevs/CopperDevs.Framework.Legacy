using CopperDevs.Framework.Components;

namespace TopDownShooter.Components;

public class PlayerPositionCopier : GameComponent
{
    public override void Update()
    {
        Transform.Position = PlayerController.Pos;
    }
}