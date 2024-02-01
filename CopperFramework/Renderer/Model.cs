using System.Numerics;
using CopperFramework.Components;
using CopperFramework.Data;
using CopperFramework.Renderer.DearImGui.Attributes;
using CopperFramework.Renderer.Internal;
using Silk.NET.OpenGL;
using InternalShader = CopperFramework.Renderer.Shader;
using InternalTexture = CopperFramework.Renderer.Texture;
using InternalModel = CopperFramework.Renderer.Internal.Model;

namespace CopperFramework.Renderer;

public class Model : GameComponent
{
    // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
    private Matrix4x4 TransformViewMatrix => Parent is not null ? Transform.Matrix : Matrix4x4.Identity;

    private static InternalShader? Shader => Framework.Shader;
    [HideInInspector] private readonly InternalModel? loadedModel;
    [HideInInspector] private readonly InternalTexture? texture;
    private static GL Gl => CopperWindow.gl;

    [ReadOnly] private string texturePath;
    [ReadOnly] private string modelPath;

    private List<Mesh> LoadedMeshes
    {
        get => loadedModel?.Meshes ?? Array.Empty<Mesh>().ToList();
        set => loadedModel!.Meshes = value;
    }

    public Model(string texturePath, string modelPath)
    {
        this.texturePath = texturePath;
        this.modelPath = modelPath;
        loadedModel = new InternalModel(Gl, modelPath);
        texture = new InternalTexture(Gl, texturePath);
    }

    public override void Render()
    {
        RenderModel(TransformViewMatrix);
    }

    protected void RenderModel(Transform transform) => RenderModel(transform.Matrix);

    private void RenderModel(Matrix4x4 modelMatrix)
    {
        foreach (var mesh in loadedModel?.Meshes!)
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
        loadedModel?.Dispose();
        texture?.Dispose();
    }
}