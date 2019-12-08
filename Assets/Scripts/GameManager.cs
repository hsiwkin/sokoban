using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject floorBox;
    public int size = 10;

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
            default:
                Debug.LogError("Unrecognized sign while reading the board");
                break;
        }
    }

    private void CreateFloorBox(float positionX, float positionZ)
    {
        float boxSize = floorBox.transform.localScale.x;
        var boxInstance = Instantiate(
            floorBox,
            new Vector3(boxSize * positionX, 0, boxSize * positionZ),
            Quaternion.identity
        );
        var boxInstanceRenderer = boxInstance.GetComponent<Renderer>();
        var material = boxInstanceRenderer.material;

        float hue, saturation, value;
        Color.RGBToHSV(material.color, out hue, out saturation, out value);

        material.color = Random.ColorHSV(hue, hue, saturation, saturation, 0.85f, 1f);
    }
}
