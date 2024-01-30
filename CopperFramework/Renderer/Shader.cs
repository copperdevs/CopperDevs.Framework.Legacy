using System.Numerics;
using Silk.NET.OpenGL;

namespace CopperFramework.Renderer;

internal class Shader : IDisposable
{
    private readonly uint handle;
    private readonly GL gl;

    public Shader(GL gl, string vertexPath, string fragmentPath)
    {
        this.gl = gl;

        var vertex = LoadShader(ShaderType.VertexShader, vertexPath);
        var fragment = LoadShader(ShaderType.FragmentShader, fragmentPath);
        handle = this.gl.CreateProgram();
        this.gl.AttachShader(handle, vertex);
        this.gl.AttachShader(handle, fragment);
        this.gl.LinkProgram(handle);
        this.gl.GetProgram(handle, GLEnum.LinkStatus, out var status);
        if (status == 0)
        {
            throw new Exception($"Program failed to link with error: {this.gl.GetProgramInfoLog(handle)}");
        }

        this.gl.DetachShader(handle, vertex);
        this.gl.DetachShader(handle, fragment);
        this.gl.DeleteShader(vertex);
        this.gl.DeleteShader(fragment);
    }

    public void Use()
    {
        gl.UseProgram(handle);
    }

    public void SetUniform(string name, int value)
    {
        var location = gl.GetUniformLocation(handle, name);

        if (location == -1)
            throw new Exception($"{name} uniform not found on shader.");

        gl.Uniform1(location, value);
    }

    public unsafe void SetUniform(string name, Matrix4x4 value)
    {
        //A new overload has been created for setting a uniform so we can use the transform in our shader.
        var location = gl.GetUniformLocation(handle, name);
        if (location == -1)
        {
            throw new Exception($"{name} uniform not found on shader.");
        }

        gl.UniformMatrix4(location, 1, false, (float*)&value);
    }

    public void SetUniform(string name, float value)
    {
        var location = gl.GetUniformLocation(handle, name);

        if (location == -1)
            throw new Exception($"{name} uniform not found on shader.");

        gl.Uniform1(location, value);
    }

    public void Dispose()
    {
        gl.DeleteProgram(handle);
    }

    private uint LoadShader(ShaderType type, string path)
    {
        var src = File.ReadAllText(path);
        var loadedShader = gl.CreateShader(type);
        gl.ShaderSource(loadedShader, src);
        gl.CompileShader(loadedShader);
        var infoLog = gl.GetShaderInfoLog(loadedShader);

        if (!string.IsNullOrWhiteSpace(infoLog))
            throw new Exception($"Error compiling shader of type {type}, failed with error {infoLog}");


        return loadedShader;
    }
}