using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Tutor_4_CreateTerrainWithSplat : MonoBehaviour {

    public AnimationCurve animationCurve;
    public Texture2D[] splats;

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
        terrainData.splatPrototypes = CreateSplatPrototypes(splats, new Vector2(15, 15), new Vector2(0, 0));
        ModifyTerrainDataHeight(terrainData);
        GameObject obj = Terrain.CreateTerrainGameObject(terrainData);
#if UNITY_EDITOR
        AssetDatabase.CreateAsset(terrainData, "Assets/Tutorial/Tutor_4_TerrainWithSplats.asset");
        AssetDatabase.SaveAssets();
#endif


        return obj.GetComponent<Terrain>();
    }

    public void ModifyTerrainDataHeight(TerrainData terrainData)
    {
        int width = terrainData.heightmapWidth;
        int height = terrainData.heightmapHeight;
        float[,] array = new float[width, height];
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

    public SplatPrototype[] CreateSplatPrototypes(Texture2D[] tmpTextures, Vector2 tmpTileSize, Vector2 tmpOffset)
    {
        SplatPrototype[] outSplatPrototypes = new SplatPrototype[tmpTextures.Length];
        for (int i = 0; i < tmpTextures.Length; i++)
        {
            outSplatPrototypes[i] = CreateSplatPrototype(tmpTextures[i], tmpTileSize, tmpOffset);
        }
        return outSplatPrototypes;
    }

    public SplatPrototype CreateSplatPrototype(Texture2D tmpTexture, Vector2 tmpTileSize, Vector2 tmpOffset)
    {
        SplatPrototype outSplatPrototype = new SplatPrototype();
        outSplatPrototype.texture = tmpTexture;
        outSplatPrototype.tileOffset = tmpOffset;
        outSplatPrototype.tileSize = tmpTileSize;
        return outSplatPrototype;
    }
}
