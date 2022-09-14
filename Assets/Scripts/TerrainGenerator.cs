using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField]
    private int depth = 20, width = 256, height = 256;
    [SerializeField]
    private float scale = 20f, offsetX =100f, offsetY = 100f;

    // Start is called before the first frame update
    void Start()
    {
        offsetX = Random.Range(0, 999f);
        offsetY = Random.Range(0, 999f);

        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain (TerrainData terrainData) {
        terrainData.heightmapResolution = width +1;
        terrainData.size = new Vector3(width, depth, height);

        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights() {
        float[,] heights = new float[width, height];
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                heights[x, y] = CalculateHeight(x, y);
            }
        }
        return heights;
    }

    float CalculateHeight(int x, int y) {
        float xCoord = (float)x/ width * scale+offsetX;
        float yCoord = (float)y/height * scale+offsetY;
        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
