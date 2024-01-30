using System.Numerics;

namespace CopperFramework.Data;

public struct Vertex
{
    public Vector3 Position;
    public Vector3 Normal;
    public Vector3 Tangent;
    public Vector2 TexCoords;
    public Vector3 Bitangent;

    public const int MaxBoneInfluence = 4;
    public int[] BoneIds;
    public float[] Weights;
}