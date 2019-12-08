using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject floorBox;
    public int size = 10;

    private void Awake()
    {
        Arrange();
    }

    private void Arrange()
    {
        for (int x = 0; x < size; ++x)
        {
            for (int z = 0; z < size; ++z)
            {
                CreateFloorBox(x, z);
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
