using Silk.NET.OpenGL;

namespace CopperFramework.Util;

public static class OpenGlUtil
{
    public static void CheckGlError(this GL gl, string title)
    {
        var error = gl.GetError();
        if (error != GLEnum.NoError)
            Log.Warning($"{title}: {error}");
    }
}