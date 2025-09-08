using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class GameManager : MonoBehaviour
{
    [Header("生成するMapのオブジェクト")]
    public GameObject square0Prefab;
    public GameObject square1Prefab;
    public GameObject square2Prefab;
    [Header("Map生成の右上角")]
    public Vector3 startPosition = new Vector3();
    [Header("Mapの生成間隔")]
    public float spacing;

    private int[,] mapData = new int[15, 29]
    {
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,0},
        {0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,1,0,0,1,0,0},
        {0,0,0,1,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,1,0,0,1,0,0,1,0,0},
        {0,0,0,1,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,1,0,0},
        {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,1,0,0},
        {0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,1,0,0},
        {0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,1,1,1,1,1,1,0,0},
        {0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,1,0,0},
        {0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,1,0,0,0,0,1,0,0},
        {0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1,0,0},
        {0,0,0,0,0,1,0,0,1,0,0,1,1,1,1,1,1,0,0,0,0,1,1,1,1,1,1,1,0},
        {0,0,0,0,0,1,0,0,1,0,0,1,0,0,0,0,1,1,1,1,1,1,0,0,0,0,0,0,0},
        {0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
    };

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        int rows = mapData.GetLength(0);
        int cols = mapData.GetLength(1);

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                Vector3 spawnPosition = startPosition + new Vector3(x * spacing, -y * spacing, 0);
                int value = mapData[y, x];

                GameObject prefabToInstantiate = value == 0 ? square0Prefab : square1Prefab;
                Instantiate(prefabToInstantiate, spawnPosition, Quaternion.identity, this.transform);
            }
        }
    }

    public bool IsWalkable(Vector2Int mapPos)
    {
        int rows = mapData.GetLength(0);
        int cols = mapData.GetLength(1);

        if (mapPos.y < 0 || mapPos.y >= rows || mapPos.x < 0 || mapPos.x >= cols)
            return false;

        return mapData[mapPos.y, mapPos.x] == 1;
    }

    
    public void UpdateMapData(Vector2Int mapPos, int newValue)
    {
        int rows = mapData.GetLength(0);
        int cols = mapData.GetLength(1);

        if (mapPos.y < 0 || mapPos.y >= rows || mapPos.x < 0 || mapPos.x >= cols)
            return;

        mapData[mapPos.y, mapPos.x] = newValue;
    }

    public Vector2Int WorldToMapPosition(Vector3 worldPosition)
    {
        int x = Mathf.RoundToInt((worldPosition.x - startPosition.x) / spacing);
        int y = Mathf.RoundToInt((startPosition.y - worldPosition.y) / spacing);
        return new Vector2Int(x, y);
    }
}
