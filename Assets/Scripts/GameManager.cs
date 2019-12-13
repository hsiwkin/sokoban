using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject floorBox, crate, wall, targetFloorBox, player;
    public Transform mainCamera;
    private GameState gameState;
    

    private void Awake()
    {
        var board1 = "board_1.txt";
        gameState = new GameState();

        new BoardBilder
        {
            floorBox = floorBox,
            crate = crate,
            wall = wall,
            targetFloorBox = targetFloorBox,
            player = player,
            gameManager = this
        }.Run(board1, gameState);

        SetupCamera();
    }
    
    private void SetupCamera()
    {
        mainCamera.transform.position = new Vector3(
            gameState.MapSize[1] / 2,
            mainCamera.transform.position.y,
            gameState.MapSize[0] / 2
        );
    }
}
