using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Tutor_2_CreateTerrain_ModifyHeight : MonoBehaviour {

    void Start()
    {
        CreateTerrain();
    }

    public Terrain CreateTerrain()
    {
        TerrainData terrainData = new TerrainData();
        terrainData.heightmapResolution = 513;
        terrainData.baseMapResolution = 513;
        terrainData.size = new Vector3(50, 50, 50);
        terrainData.alphamapResolution = 512;
        terrainData.SetDetailResolution(32, 8);
        ModifyTerrainDataHeight(terrainData);
        GameObject obj = Terrain.CreateTerrainGameObject(terrainData);
#if UNITY_EDITOR
        AssetDatabase.CreateAsset(terrainData, "Assets/Tutorial/Tutor_2_Terrain_ModifyHeight.asset");
        AssetDatabase.SaveAssets();
#endif
        return obj.GetComponent<Terrain>();
    }

    public void ModifyTerrainDataHeight(TerrainData terrainData)
    {
        int width = terrainData.heightmapWidth;
        int height = terrainData.heightmapHeight;
        float[,] array = new float[width, height];
        print("width:" + width + " height:" + height);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                float f1 = i;
                float f2 = width;
                float f3 = j;
                float f4 = height;
                float baseV = (f1 / f2 + f3 / f4) / 2 * 1;
                array[i, j] = baseV * baseV;
            }
        }
        terrainData.SetHeights(0, 0, array);
    }
}
