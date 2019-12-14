using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float smoothSpeed = 0.125f;
    public Vector3 offset, rotation;

    private GameState gameState;

    private void Awake()
    {
        gameState = GameState.Instance;
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = getPlayer().transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * 10 * Time.deltaTime
        );

        transform.position = smoothedPosition;
        //transform.LookAt(getPlayer().transform);
        transform.eulerAngles = rotation;
    }

    private GameObject getPlayer()
    {
        return gameState.mapData[
            gameState.playerPosition[0],
            gameState.playerPosition[1]
        ].item;
    }
}
