using CopperDevs.Framework.Resources.Core;

namespace CopperDevs.Framework.Resources.Fonts;

public class FontRegistry : ResourceRegistry<FontRegistry>
{
    public FontRegistry() : base(ResourceType.Fonts) => SetInstance(this);

    public Figtree Figtree = null!;
    public Inter Inter = null!;

    protected override void LoadResources()
    {
        Figtree = new Figtree();
        Inter = new Inter();
    }
}