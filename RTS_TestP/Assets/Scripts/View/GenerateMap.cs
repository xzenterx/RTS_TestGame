using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateMap : MonoBehaviour
{

    public int SizeX = 15;
    public int SizeY = 15;

    public Tilemap GrassMap;
    public TileBase GrassTile;

    public GameObject BaseObj;

    private void Awake()
    {
        GenerateGrass();
        CreateBasePosition();
    }

    private void Start()
    {
        
    }

    private void GenerateGrass()
    {

        Vector3Int[] positions = new Vector3Int[SizeX * SizeY];
        TileBase[] tilesMap = new TileBase[positions.Length];

        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = new Vector3Int(i % SizeX, i / SizeY, 0);
            tilesMap[i] = GrassTile;

        }

        GrassMap.SetTiles(positions, tilesMap);
    }

    private void CreateBasePosition()
    {
        float posX;
        float posY;

        posX = Random.Range(0, SizeX - 1) + 0.5f;
        posY = Random.Range(0, SizeY - 1) + 0.5f;

        Instantiate(BaseObj, new Vector3(posX, posY, 0), Quaternion.identity);
    }
}
