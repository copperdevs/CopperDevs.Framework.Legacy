using Raylib_CSharp.Textures;

namespace CopperDevs.Framework.Rendering;

public class Font : BaseRenderable
{
    // rl data
    private rlFont font;
    
    // info
    public readonly string Name;
    public int BaseSize => font.BaseSize;
    public int GlyphCount => font.GlyphCount;
    public int GlyphPadding => font.GlyphPadding;
    public rlTexture Texture => font.Texture;

    // loading stuff
    private readonly byte[]? fontData;
    private readonly string? fontPath;


    public static Font Load(string fontName, byte[] fontData) => new(fontName, fontData);
    public Font(string fontName, byte[] fontData) : this(fontName) => this.fontData = fontData;

    public static Font Load(string fontName, string fontPath) => new(fontName);
    public Font(string fontName, string fontPath) : this(fontName) => this.fontPath = fontPath;

    public static Font Load(string fontName = "Default") => new(fontName);
    public Font(string fontName)
    {
        Name = fontName;
        BaseLoad(this);
    }

    public override void LoadRenderable()
    {
        if (fontData is not null)
            font = rlFont.LoadFromMemory(".ttf", fontData, 144, null);
        else if (fontPath is not null)
            font = rlFont.Load(fontPath);
        else
            font = rlFont.GetDefault();
        
        RenderingSystem.Instance.RegisterRenderableItem(this);
    }

    public override void UnLoadRenderable()
    {
        RenderingSystem.Instance.DeregisterRenderableItem(this);
        font.Unload();
    }

    public static implicit operator rlFont(Font font) => font.font;
}