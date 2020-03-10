using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Tutor_5_CreateTerrainWithSplat : MonoBehaviour {

    public AnimationCurve animationCurve;
    public Texture2D[] splats;

    void Start()
    {
        CreateTerrain();

        TerrainData terData = AssetDatabase.LoadAssetAtPath("Assets/Tutorial/Tutor_5_TerrainWithSplats.asset", typeof(TerrainData)) as TerrainData;
        float[, ,] splatAlphaArray = CreateSplatAlphaArray(splatAlphaMaps, terData.splatPrototypes.Length);
        terData.SetAlphamaps(0, 0, splatAlphaArray);
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
        AssetDatabase.CreateAsset(terrainData, "Assets/Tutorial/Tutor_5_TerrainWithSplats.asset");
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

    public Texture2D[] splatAlphaMaps;

    public float[, ,] CreateSplatAlphaArray(Texture2D[] splatAlphaMaps, int numOfSplatPrototypes)
    {
        List<Color> cArray = new List<Color>();
        int splatAlphaMap_SizeX = splatAlphaMaps[0].width;
        int splatAlphaMap_SizeY = splatAlphaMaps[0].height;
        float[, ,] outSplatAlphaArray = new float[splatAlphaMap_SizeX, splatAlphaMap_SizeY, numOfSplatPrototypes];

        //第几张SplatAlphaMap
        for (int splatAlphaMapIndex = 0; splatAlphaMapIndex < splatAlphaMaps.Length; splatAlphaMapIndex++)
        {
            //RGBA第几个通道
            for (int alphaIndex = 0; alphaIndex < 4; alphaIndex++)
            {
                //Splat ID
                int splatIndex = alphaIndex + splatAlphaMapIndex * 4;
                //仅当Splat ID小于Splat的数量时
                if (splatIndex < numOfSplatPrototypes)
                {
                    for (int index_heightmapY = 0; index_heightmapY < splatAlphaMap_SizeY; index_heightmapY++)
                    {
                        for (int index_heightmapX = 0; index_heightmapX < splatAlphaMap_SizeX; index_heightmapX++)
                        {
                            //取第splatAlphaMapIndex张SplatAlphaMap上的位于index_heightmapY，index_heightmapX的颜色值
                            Color c = splatAlphaMaps[splatAlphaMapIndex].GetPixel(index_heightmapY, index_heightmapX);
                            cArray.Add(c);
                            //赋予outSplatAlphaArray的index_heightmapX，index_heightmapY,splatIndex对应的通道值
                            outSplatAlphaArray[index_heightmapX, index_heightmapY, splatIndex] = c[alphaIndex];
                        }
                    }
                }
                else
                {
                    return outSplatAlphaArray;
                }
            }
        }
        return outSplatAlphaArray;
    }
}
