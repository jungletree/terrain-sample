using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Tutor_3_CreateTerrainWithAnimationCurve : MonoBehaviour {

    public AnimationCurve animationCurve;

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
        AssetDatabase.CreateAsset(terrainData, "Assets/Tutorial/Tutor_3_TerrainWithAnimationCurve.asset");
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
                float f1 = j;
                float f2 = height;
                array[i, j] = animationCurve.Evaluate(f1 / f2);
            }
        }
        terrainData.SetHeights(0, 0, array);
    }
}
