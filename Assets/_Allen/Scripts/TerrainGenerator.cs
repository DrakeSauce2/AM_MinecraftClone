using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private int chunkLimitX;
    [SerializeField] private int chunkLimitZ;
    [Space]
    private int xSize = 16, zSize = 16;
    [SerializeField] private float terrainDetail;
    [SerializeField] private float terrainHeight;

    private int offsetX = 0;
    private int offsetZ = 0;
    private int maxY;

    int seed;

    [SerializeField] private List<GameObject> blocks = new List<GameObject>();
    [SerializeField] private GameObject player;

    GameObject placeHolder = null;
    private bool isSpawned = false;


    private void Start()
    {
        seed = Random.Range(100000, 999999);
        BuildWorld();
    }

    private void BuildWorld()
    {
        for (int i = 0; i < chunkLimitX; i++)
        {
            offsetX = (i * xSize);
            for (int j = 0; j < chunkLimitZ; j++)
            {             
                offsetZ = (j * zSize);

                //maxY = (int)(Mathf.PerlinNoise((i / 2 + seed) / terrainDetail, (j / 2 + seed) / terrainDetail) * terrainHeight);

                GenerateChunk(xSize, zSize);
            }
        }
    }

    
    private void GenerateChunk(int chunkSizeX, int  chunkSizeZ)
    {
        Vector3 center = new Vector3(((chunkSizeX / 2) - 0.5f), (terrainHeight / 2) - 0.5f, ((chunkSizeZ / 2) - 0.5f));

        GameObject chunk = new GameObject("Chunk");
        chunk.transform.position = center;
        //Instantiate(chunk, center, Quaternion.identity);

        chunk.AddComponent<OcclusionArea>();

        chunk.GetComponent<OcclusionArea>().size = new Vector3(chunkSizeX, terrainHeight, chunkSizeZ);
        chunk.GetComponent<OcclusionArea>().center = new Vector3(offsetX, 0, offsetZ);

        placeHolder = chunk;
        placeHolder.transform.SetParent(GameObject.FindGameObjectWithTag("Environment").transform);

        GenerateTerrain(chunkSizeX, chunkSizeZ);
    }

    private void GenerateTerrain(int sizeX, int sizeZ)
    {
        for (int x = 0; x < sizeX; x++)
        {
            for(int z = 0; z < sizeZ; z++)
            {
                PlaceBlock(x, z);
            }
        }
    }

    private void PlaceBlock(int x, int z)
    {
        int maxY = (int)(Mathf.PerlinNoise((x / 2 + seed + offsetX) / terrainDetail, (z / 2 + seed + offsetZ) / terrainDetail) * terrainHeight);
        GameObject grass = Instantiate(blocks[0], new Vector3(x + offsetX, maxY, z + offsetZ), Quaternion.identity) as GameObject;
        grass.transform.SetParent(placeHolder.transform);

        for (int y = 0; y < maxY; y++)
        {
            int dirtLayers = Random.Range(1, 5);
            if (y >= maxY - dirtLayers)
            {
                GameObject dirt = Instantiate(blocks[1], new Vector3(x + offsetX, y, z + offsetZ), Quaternion.identity) as GameObject;
                dirt.transform.SetParent(placeHolder.transform);
            }
            else
            {
                GameObject stone = Instantiate(blocks[2], new Vector3(x + offsetX, y, z + offsetZ), Quaternion.identity) as GameObject;
                stone.transform.SetParent(placeHolder.transform);
            }
        }

        if (isSpawned) return;

        if (x == (int)(x / 2) && z == (int)(z / 2))
        {
            isSpawned = true;
            Instantiate(player, new Vector3(x, maxY + 3, z), Quaternion.identity);
        }
    }

}
