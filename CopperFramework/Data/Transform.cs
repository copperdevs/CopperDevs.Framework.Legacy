using System.Numerics;

namespace CopperFramework.Data;

public struct Transform
{
    public Transform()
    {
        Position = Vector3.Zero;
        Scale = Vector3.One;
        Rotation = Quaternion.Identity;
    }

    public Vector3 Position { get; set; } = Vector3.Zero;

    public Vector3 Scale { get; set; } = Vector3.One;

    public Quaternion Rotation { get; set; } = Quaternion.Identity;

    //Note: The order here does matter.
    public Matrix4x4 Matrix
    {
        get => GetMatrix();
        set => SetMatrix(value);
    }

    public static implicit operator Matrix4x4(Transform transform) => transform.Matrix;

    private Matrix4x4 GetMatrix()
    {
        return Matrix4x4.Identity *
               Matrix4x4.CreateFromQuaternion(Rotation) *
               Matrix4x4.CreateScale(Scale) *
               Matrix4x4.CreateTranslation(Position);
    }

    private void SetMatrix(Matrix4x4 matrix)
    {
        Position = matrix.Translation;

        var withoutTranslation = matrix;
        withoutTranslation.Translation = Vector3.Zero;

        Vector3 extractedScale;
        extractedScale.X = new Vector3(withoutTranslation.M11, withoutTranslation.M12, withoutTranslation.M13).Length();
        extractedScale.Y = new Vector3(withoutTranslation.M21, withoutTranslation.M22, withoutTranslation.M23).Length();
        extractedScale.Z = new Vector3(withoutTranslation.M31, withoutTranslation.M32, withoutTranslation.M33).Length();
        Scale = extractedScale;

        Rotation = Quaternion.CreateFromRotationMatrix(Matrix4x4.Transpose(withoutTranslation));
    }
}