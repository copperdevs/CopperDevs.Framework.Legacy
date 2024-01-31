using System.Numerics;
using CopperFramework;
using CopperFramework.Components;
using CopperFramework.Data;
using CopperFramework.Renderer.Internal;
using ImGuiNET;
using Silk.NET.OpenGL;
using InternalShader = CopperFramework.Renderer.Shader;
using InternalTexture = CopperFramework.Renderer.Texture;
using InternalModel = CopperFramework.Renderer.Internal.Model;

namespace CopperEngine.Rendering;

public class Model : GameComponent
{
    private Matrix4x4 TransformViewMatrix => Transform.Matrix;
    private static InternalShader? Shader => Framework.Shader;
    private InternalModel? LoadedModel { get; set; }
    private readonly InternalTexture? texture;
    private static GL Gl => CopperWindow.gl;

    private List<Mesh> LoadedMeshes
    {
        get => LoadedModel?.Meshes ?? Array.Empty<Mesh>().ToList();
        set => LoadedModel!.Meshes = value;
    }

    public Model(string texturePath, string modelPath)
    {
        LoadedModel = new InternalModel(Gl, modelPath);
        texture = new InternalTexture(Gl, texturePath);
    }

    public override void Render()
    {
        RenderModel(TransformViewMatrix);
    }

    protected void RenderModel(Transform transform) => RenderModel(transform.Matrix);

    private void RenderModel(Matrix4x4 modelMatrix)
    {
        foreach (var mesh in LoadedModel?.Meshes!)
        {
            mesh.Bind();
            Shader?.Use();
            texture?.Bind();
            Shader?.SetUniform("uTexture0", 0);
            Shader?.SetUniform("uModel", modelMatrix);
            Shader?.SetUniform("uView", Camera.ViewMatrix);
            Shader?.SetUniform("uProjection", Camera.ProjectionMatrix);

            Gl.DrawArrays(PrimitiveType.Triangles, 0, (uint)mesh.Vertices.Length);
        }
    }

    public override void Stop()
    {
        Dispose();
    }

    private void Dispose()
    {
        LoadedModel?.Dispose();
        texture?.Dispose();
    }
}