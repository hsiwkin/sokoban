using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public Vector2Int playerPosition; // (height, width)
    private Vector2Int mapSize;
    public Cell[,] mapData;

    private static readonly GameState instance = new GameState();

    static GameState() {}

    private GameState() {}

    public static GameState Instance
    {
        get
        {
            return instance;
        }
    }

    public Vector2Int MapSize
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

    public Cell this[int height, int width]
    {
        get
        {
            return mapData[height, width];
        }

        set
        {
            mapData[height, width] = value;
        }
    }

    private void initializeMapData(Vector2Int size)
    {
        int height = size[0];
        int width = size[1];

        mapData = new Cell[height, width];  
    }
}
