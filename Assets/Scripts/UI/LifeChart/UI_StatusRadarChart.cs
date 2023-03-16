using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StatusRadarChart : MonoBehaviour
{
    [SerializeField] private Material radarMaterial;
    [SerializeField] private Texture2D radarTexture2D;
    private Stats stats;
    private CanvasRenderer radarMeshCanvasRenderer;

    private void Awake()
    {
        radarMeshCanvasRenderer = transform.Find("radarMesh").GetComponent<CanvasRenderer>();
    }
    public void SetStats(Stats stats)
    {
        this.stats = stats;
        stats.OnStatsChanged += Stats_OnStatsChanged;
        UpdateStatsVisual();
    }

    private void Stats_OnStatsChanged(object sender, System.EventArgs e)
    {
        UpdateStatsVisual();
    }

    private void UpdateStatsVisual()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[6];
        Vector2[] uv = new Vector2[6];
        int[] triangles = new int[3 * 5];

        float angleIncrement = 360f / 5;
        float radarChartSize = 90.71f;

        Vector3 credibilityVertex = Quaternion.Euler(0,0, -angleIncrement * 0 ) * Vector3.up * radarChartSize * stats.GetStatAmountNormalized(Stats.Type.Credibility);
        int credibilityVertexIndex = 1;
        Vector3 stabilityVertex = Quaternion.Euler(0, 0, -angleIncrement * 1) * Vector3.up * radarChartSize * stats.GetStatAmountNormalized(Stats.Type.Stability);
        int stabilityVertexIndex = 2;
        Vector3 healthVertex = Quaternion.Euler(0, 0, -angleIncrement * 2) * Vector3.up * radarChartSize * stats.GetStatAmountNormalized(Stats.Type.Health);
        int healthVertexIndex = 3;
        Vector3 happinessVertex = Quaternion.Euler(0, 0, -angleIncrement * 3) * Vector3.up * radarChartSize * stats.GetStatAmountNormalized(Stats.Type.Happiness);
        int happinessVertexIndex = 4;
        Vector3 riskVertex = Quaternion.Euler(0, 0, -angleIncrement * 4) * Vector3.up * radarChartSize * stats.GetStatAmountNormalized(Stats.Type.risk);
        int riskVertexIndex = 5;

        vertices[0] = Vector3.zero;
        vertices[credibilityVertexIndex] = credibilityVertex;
        vertices[stabilityVertexIndex] = stabilityVertex;
        vertices[healthVertexIndex] = healthVertex;
        vertices[happinessVertexIndex] = happinessVertex;
        vertices[riskVertexIndex] = riskVertex;

        /* // เตรียมไว้สำหรับเส้น กรอบ
        uv[0] = Vector2.zero;
        uv[credibilityVertexIndex] = Vector2.one;
        uv[stabilityVertexIndex] = Vector2.one;
        uv[healthVertexIndex] = Vector2.one;
        uv[happinessVertexIndex] = Vector2.one;
        uv[riskVertexIndex] = Vector2.one; */

        triangles[0] = 0;
        triangles[1] = credibilityVertexIndex;
        triangles[2] = stabilityVertexIndex;

        triangles[3] = 0;
        triangles[4] = stabilityVertexIndex;
        triangles[5] = healthVertexIndex;

        triangles[6] = 0;
        triangles[7] = healthVertexIndex;
        triangles[8] = happinessVertexIndex;

        triangles[9] = 0;
        triangles[10] = happinessVertexIndex;
        triangles[11] = riskVertexIndex;

        triangles[12] = 0;
        triangles[13] = riskVertexIndex;
        triangles[14] = credibilityVertexIndex;

        mesh.vertices = vertices;
        mesh.uv = uv;   
        mesh.triangles = triangles;

        radarMeshCanvasRenderer.SetMesh(mesh);
        radarMeshCanvasRenderer.SetMaterial(radarMaterial, null);
    }
}
