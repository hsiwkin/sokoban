using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public Vector2Int playerPosition;
    public Vector2Int mapSize;
    public Cell[,] mapData;

    Vector2Int MapSize
    {
        get
        {
            return mapSize;
        }

        set
        {
            mapSize = value;
            initializeMapData(mapSize);
        }
    }

    public Cell this[int x, int y]
    {
        get
        {
            return mapData[x, y];
        }

        set
        {
            mapData[x, y] = value;
        }
    }

    private void initializeMapData(Vector2Int size)
    {
        int height = size[0];
        int width = size[1];

        mapData = new Cell[height, width];  
    }
}
