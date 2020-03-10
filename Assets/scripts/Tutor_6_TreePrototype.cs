using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Tutor_6_TreePrototype : MonoBehaviour {

    public GameObject treePrefab;
    public TerrainData terrainData;

    void Start()
    {
        Terrain terrain = CreateTerrain();

        AddTrees(terrain, 100);
    }

    public Terrain CreateTerrain()
    {
        TerrainData terrainData = new TerrainData();
        terrainData.heightmapResolution = 513;
        terrainData.baseMapResolution = 513;
        terrainData.size = new Vector3(50, 50, 50);
        terrainData.alphamapResolution = 512;
        terrainData.SetDetailResolution(32, 8);
        TreePrototype treePrototype = new TreePrototype();
        treePrototype.prefab = treePrefab;
        treePrototype.bendFactor = 1;

        // Error !!!!
        //terrainData.treePrototypes = new TreePrototype[1];
        //terrainData.treePrototypes[0] = treePrototype;

        terrainData.treePrototypes = new TreePrototype[1] { treePrototype };

        GameObject obj = Terrain.CreateTerrainGameObject(terrainData);
#if UNITY_EDITOR
        AssetDatabase.CreateAsset(terrainData, "Assets/Tutorial/Tutor_6_SimpleTerrain.asset");
        AssetDatabase.SaveAssets();
#endif
        return obj.GetComponent<Terrain>();
    }

    public void AddTrees(Terrain terrain, int numOfTrees)
    {
        if (terrain.terrainData != null)
        {
            terrain.terrainData.treeInstances = new TreeInstance[numOfTrees];
            for (int i = 0; i < numOfTrees; i++)
            {
                TreeInstance tmpTreeInstances = new TreeInstance();
                tmpTreeInstances.prototypeIndex = 0; // ID of tree prototype
                tmpTreeInstances.position = new Vector3(Random.Range(0f, 1f), 0, Random.Range(0f, 1f));   // not regular pos,  [0,1]
                tmpTreeInstances.color = new Color(1, 1, 1, 1);
                tmpTreeInstances.lightmapColor = new Color(1, 1, 1, 1);//must add
                float ss = Random.Range(0.8f, 1f);
                tmpTreeInstances.heightScale = ss; //same size as prototype
                tmpTreeInstances.widthScale = ss;
                terrain.AddTreeInstance(tmpTreeInstances);
            }
            TerrainCollider tc = terrain.GetComponent<TerrainCollider>();
            tc.enabled = false;
            tc.enabled = true;
        }
    }
}
