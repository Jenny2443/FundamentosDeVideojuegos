using UnityEngine;

public class TerrainScript : MonoBehaviour
{
    public Terrain terrain;
    public Texture2D heightmap;  // Puedes crear una textura para controlar la altura del terreno
    public int terrainWidth = 100;
    public int terrainLength = 100;

    public float heightScale = 10.0f;  // Factor de escala para la altura del terreno

    void Start()
    {
        if (terrain == null)
        {
            Debug.LogError("Terrain not assigned! Please assign a terrain.");
            return;
        }

        if (heightmap == null)
        {
            Debug.LogError("Heightmap not assigned! Please assign a heightmap texture.");
            return;
        }

        // Asigna la altura del terreno basada en la textura de altura
        SetTerrainHeight();

        // Puedes agregar más funcionalidades aquí, como la colocación de árboles, hierba, etc.
    }

    private void SetTerrainHeight()
    {
        float[,] heights = new float[terrainWidth, terrainLength];

        for (int i = 0; i < terrainWidth; i++)
        {
            for (int j = 0; j < terrainLength; j++)
            {
                // Obtén la altura de la textura de altura y escálala
                float height = heightmap.GetPixel(i, j).r * heightScale;

                // Normaliza la altura al rango 0-1
                heights[j, i] = height / heightScale;
            }
        }

        // Asigna la altura al terreno
        terrain.terrainData.SetHeights(0, 0, heights);
    }
}
