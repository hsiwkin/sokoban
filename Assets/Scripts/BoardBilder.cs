﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BoardBilder
{
    public GameObject floorBox, crate, wall, targetFloorBox, player;
    public GameManager gameManager;
    private GameState gameState;

    public void Run(string board, GameState gameState)
    {
        this.gameState = gameState;

        var rows = board.Split('\n');
        System.Array.Reverse(rows);

        int rowsCount = rows.Length;
        int columnsCount = rowsCount > 0 ? rows[0].Length : 0;
        this.gameState.MapSize = new Vector2Int(rowsCount, columnsCount);

        for (int height = 0; height < rows.Length; ++height)
        {
            var row = rows[height].ToCharArray();

            for (int width = 0; width < row.Length; ++width)
            {
                CreateElement(row[width], width, height);
            }
        }
    }

    private void CreateElement(char elementSign, int width, int height)
    {
        var cell = new Cell();
        switch (elementSign)
        {
            case '.':
                CreateFloorBox(width, height);
                cell.type = CellType.Floor; 
                break;
            case '#':
                CreateWall(width, height);
                cell.type = CellType.Wall;
                break;
            case '@':
                CreateFloorBox(width, height);
                var crateInstance = CreateCrate(width, height);
                cell.type = CellType.Crate;
                cell.item = crateInstance;
                break;
            case 'X':
                CreateTargetLocation(width, height);
                cell.type = CellType.TargetSpot;
                break;
            case '$':
                CreateFloorBox(width, height);
                var playerInstance = CreatePlayer(width, height);
                cell.type = CellType.Player;
                cell.item = playerInstance;
                gameState.playerPosition = new Vector2Int(height, width);
                break;
            default:
                Debug.LogError("Unrecognized sign while reading the board");
                break;
        }
        gameState[height, width] = cell;
    }

    private GameObject CreateFloorElement(int width, int height, GameObject prefab)
    {
        
        var boxInstance = Object.Instantiate(
            prefab,
            new Vector3(width, -0.5f * floorBox.transform.localScale.y, height),
            Quaternion.identity
        );

        var boxInstanceRenderer = boxInstance.GetComponent<Renderer>();
        var material = boxInstanceRenderer.material;

        float hue, saturation, value;
        Color.RGBToHSV(material.color, out hue, out saturation, out value);

        material.color = Random.ColorHSV(
            hue, hue, saturation, saturation, 0.85f, 1f);

        return boxInstance;
    }

    private GameObject CreateFloorBox(int width, int height)
    {
        return CreateFloorElement(width, height, floorBox);
    }

    private GameObject CreateTargetLocation(int width, int height)
    {
        return CreateFloorElement(width, height, targetFloorBox);
    }

    private GameObject CreateWall(int width, int height)
    {
        var wallInstance = Object.Instantiate(
            wall,
            new Vector3(width, .5f, height),
            Quaternion.identity
        );


        wallInstance.transform.localScale =
            wallInstance.transform.localScale +
            new Vector3(0, Random.Range(0.1f, 0.5f), 0);

        return wallInstance;
    }

    private GameObject CreateCrate(int width, int height)
    {
        gameState.TotalCratesCount++;

        return Object.Instantiate(
            crate,
            new Vector3(width, .5f, height),
            Quaternion.identity
        );
    }

    private GameObject CreatePlayer(int width, int height)
    {
        return Object.Instantiate(
            player,
            new Vector3(width, 0, height),
            Quaternion.identity
        );
    }
}
