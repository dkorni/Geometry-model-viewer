using GMV.Core.DrawSrtrategies;
using UnityEngine;
using UnityEngine.UIElements;

public class Figure
{
    public Mesh Mesh { get; private set; }

    private readonly DrawSrtrategy drawSrtrategy;
    private readonly Vector3[] vericesInitialPoints;

    private readonly Vector3 offsetFromZeroVector;

    private Vector3 lastOffset;

    private Figure(Mesh mesh, DrawSrtrategy drawSrtrategy, Vector3[] vericesInitialPoints, Vector3 offsetFromZeroVector)
    {
        Mesh = mesh;
        this.drawSrtrategy = drawSrtrategy;
        this.vericesInitialPoints = vericesInitialPoints;
        this.offsetFromZeroVector = offsetFromZeroVector;
    }

    public static Figure Build(DrawSrtrategy drawSrtrategy)
    {
        var mesh = drawSrtrategy.Draw();
        return new Figure(mesh, drawSrtrategy, mesh.vertices, Vector3.zero);
    }

    public void MakeEuclideanTransformations(Vector3 translation, Vector3 angles)
    {
        var newVertices = new Vector3[vericesInitialPoints.Length];

        for (int i = 0; i < vericesInitialPoints.Length; i++)
        {
            var initialVertice = vericesInitialPoints[i];

            // https://docs.unity3d.com/ScriptReference/Matrix4x4.Translate.html
            // https://docs.unity3d.com/ScriptReference/Matrix4x4.Rotate.html
            Matrix4x4 m = Matrix4x4.Translate(translation) * Matrix4x4.Rotate(Quaternion.Euler(angles.x, angles.y, angles.z));

            Vector3 rotatedVector = m.MultiplyPoint3x4(initialVertice);

            newVertices[i] = rotatedVector;
        }

        Mesh.SetVertices(newVertices);
        Mesh.RecalculateBounds();
    }
}