using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Tutor_1_CreateSimpleTerrain : MonoBehaviour {

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
        GameObject obj = Terrain.CreateTerrainGameObject(terrainData);
#if UNITY_EDITOR
        AssetDatabase.CreateAsset(terrainData, "Assets/Tutorial/Tutor_1_SimpleTerrain.asset");
        AssetDatabase.SaveAssets();
#endif
        return obj.GetComponent<Terrain>();
    }

}
