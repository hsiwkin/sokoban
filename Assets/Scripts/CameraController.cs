using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    private GameState gameState;

    private void Awake()
    {
        gameState = GameState.Instance;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(
            gameState.playerPosition.y,
            5,
            gameState.playerPosition.x
        );
    }
}
