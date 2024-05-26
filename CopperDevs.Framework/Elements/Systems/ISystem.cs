namespace CopperDevs.Framework.Elements.Systems;

internal interface ISystem
{
    public int GetPriority();
    public SystemUpdateType GetUpdateType();

    public void Start();
    public void Update();
    public void UiUpdate();
    public void Stop();
}