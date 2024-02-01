using System.Numerics;
using CopperFramework.Util;

namespace CopperFramework.Data;

public class Transform
{
    public Vector3 Position = Vector3.Zero;

    public Vector3 Scale = Vector3.One;

    public Vector3 Rotation = Vector3.Zero;


    public Vector3 RadiansRotation
    {
        get => Rotation;
        set => Rotation = value;
    }

    public Vector3 DegreesRotation
    {
        get => MathUtil.RadiansToDegrees(RadiansRotation);
        set => RadiansRotation = MathUtil.DegreesToRadians(value);
    }

    public Matrix4x4 RotationMatrix => Matrix4x4.CreateFromYawPitchRoll
    (
        Rotation.Y,
        Rotation.X,
        Rotation.Z
    );


    public Vector3 Forward => new(RotationMatrix.M13, RotationMatrix.M23, RotationMatrix.M33);
    public Vector3 Up => new(RotationMatrix.M12, RotationMatrix.M22, RotationMatrix.M32);

    public Matrix4x4 Matrix => GetMatrix();

    public static implicit operator Matrix4x4(Transform transform) => transform.Matrix;

    private Matrix4x4 GetMatrix()
    {
        return Matrix4x4.Identity *
               Matrix4x4.CreateFromYawPitchRoll(Rotation.Y, Rotation.X, Rotation.Z) *
               Matrix4x4.CreateScale(Scale) *
               Matrix4x4.CreateTranslation(Position);
    }
}