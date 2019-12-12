﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject floorBox, crate, wall, targetFloorBox, player;
    private float elementSize = 1f;

    private void Awake()
    {
        var board1 = "board_1.txt";

        LoadBoard(board1);
    }
    
    private void LoadBoard(string boardName)
    {
        var boardPath = Path.Combine(Application.dataPath, "Boards", boardName);
        var streamReader = new StreamReader(boardPath);
        var fileContents = streamReader.ReadToEnd();
        streamReader.Close();

        var rows = fileContents.Split('\n');
        System.Array.Reverse(rows);

        for (int z = 0; z < rows.Length; ++z)
        {
            var row = rows[z].ToCharArray();

            for (int x = 0; x < row.Length; ++x)
            {
                CreateElement(row[x], x, z);
            }
        }
    }

    private void CreateElement(char elementSign, float positionX, float positionZ)
    {
        switch (elementSign)
        {
            case '.':
                CreateFloorBox(positionX, positionZ);
                break;
            case '#':
                CreateWall(positionX, positionZ);
                break;
            case '@':
                CreateFloorBox(positionX, positionZ);
                CreateCrate(positionX, positionZ);
                break;
            case 'X':
                CreateTargetLocation(positionX, positionZ);
                break;
            case '$':
                CreateFloorBox(positionX, positionZ);
                CreatePlayer(positionX, positionZ);
                break;
            default:
                Debug.LogError("Unrecognized sign while reading the board");
                break;
        }
    }

    private void CreateFloorElement(float positionX, float positionZ, GameObject prefab)
    {
        var boxInstance = Instantiate(
            prefab,
            new Vector3(elementSize * positionX, -0.5f * floorBox.transform.localScale.y, elementSize * positionZ),
            Quaternion.identity
        );

        var boxInstanceRenderer = boxInstance.GetComponent<Renderer>();
        var material = boxInstanceRenderer.material;

        float hue, saturation, value;
        Color.RGBToHSV(material.color, out hue, out saturation, out value);

        material.color = Random.ColorHSV(hue, hue, saturation, saturation, 0.85f, 1f);
    }

    private void CreateFloorBox(float positionX, float positionZ)
    {
        CreateFloorElement(positionX, positionZ, floorBox);
    }

    private void CreateTargetLocation(float positionX, float positionZ)
    {
        CreateFloorElement(positionX, positionZ, targetFloorBox);
    }

    private void CreateWall(float positionX, float positionZ)
    {
        var wallInstance = Instantiate(
            wall,
            elementSize * new Vector3(positionX, .5f, positionZ),
            Quaternion.identity
        );


        wallInstance.transform.localScale =
            wallInstance.transform.localScale +
            new Vector3(0, Random.Range(0.1f, 0.5f), 0);
    }

    private void CreateCrate(float positionX, float positionZ)
    {
        Instantiate(
            crate,
            elementSize * new Vector3(positionX, .5f, positionZ),
            Quaternion.identity
        );
    }

    private void CreatePlayer(float positionX, float positionZ)
    {
        // TODO
        Instantiate(
            player,
            elementSize * new Vector3(positionX, 0, positionZ),
            Quaternion.identity
        );
    }
}
