using CopperFramework.Elements.Systems;

namespace CopperFramework.UiCreator;

public class UiCreatorUiManagerSystem : BaseSystem<UiCreatorUiManagerSystem>
{
    public override SystemUpdateType GetUpdateType() => SystemUpdateType.UiRenderer;

    public override void UpdateSystem()
    {
    }
}