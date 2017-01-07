using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class ProcedualRandomMesh : MonoBehaviour
{
    // 三角形の個数
    private int triangleCount = 1500;

    // 三角形の大きさ
    private float triangleScale = 0.002f;

    // メッシュの大きさ
    private Vector3 meshScale = new Vector3(4f, 4f, 4f);

    /// <summary>
    /// 雨のランダムさ
    /// </summary>
    private float randomness = 0.1f;

    private void Start()
    {
        this.Initialize();
    }

    /// <summary>
    /// 初期化 
    /// </summary>
    public void Initialize()
    {
        var vertices = new List<Vector3>();
        var triangles = new List<int>();
        int pos = 0;
        for (int i = 0; i < this.triangleCount; i++)
        {
            var v1 = Vector3.Scale(new Vector3(Random.value, Random.value, Random.value) - Vector3.one * 0.5f, this.meshScale);
            var v2 = v1 + new Vector3(Random.value - 0.5f, 0f, Random.value - 0.5f) * triangleScale;
            var v3 = v1 + new Vector3(Random.value - 0.5f, 0f, Random.value - 0.5f) * triangleScale;

            vertices.Add(v1);
            vertices.Add(v2);
            vertices.Add(v3);

            triangles.Add(pos + 0);
            triangles.Add(pos + 1);
            triangles.Add(pos + 2);
            pos += 3;
        }

        // メッシュを生成
        var mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        var meshFilter = this.GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;
    }

    void Update()
    {
        // 雨を揺らす
        this.transform.position = new Vector3(Random.value, 0f, Random.value) * randomness;
    }
}