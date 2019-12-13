using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject floorBox, crate, wall, targetFloorBox, player;
    public Transform mainCamera;
    private GameState gameState;

    private bool performingAction = false;
    private Vector3Int targetPosition;
    public float speed = 1.0f;


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

    private void Update()
    {
        HandleInput();

        if (performingAction)
        {
            PerformAction();
        }
    }

    private void SetupCamera()
    {
        mainCamera.transform.position = new Vector3(
            gameState.MapSize[1] / 2,
            mainCamera.transform.position.y,
            gameState.MapSize[0] / 2
        );
    }

    private void HandleInput()
    {
        // TODO: it's a mock - implement it

        if (!performingAction)
        {
            var verticalInput = Input.GetAxis("Vertical");
            var horizontalInput = Input.GetAxis("Horizontal");

            if (Mathf.Abs(horizontalInput) > 0)
            {
                MoveRight();
            }
        }
    }

    private void MoveRight()
    {
        performingAction = true;
        var position = gameState.playerPosition;
        var width = position[1];
        var height = position[0];

        targetPosition = new Vector3Int(width + 1, 0, height);

        //Debug.Log("target width: " + (width + 1));
        //Debug.Log("target height: " + height);

        PerformAction();
    }

    private void PerformAction()
    {
        float step = speed * Time.deltaTime;
        var playerInstance = gameState.mapData[gameState.playerPosition[0], gameState.playerPosition[1]].item;

        playerInstance.transform.position = Vector3.MoveTowards(playerInstance.transform.position, targetPosition, step);

        Debug.Log("player: " + playerInstance.transform.position);
        Debug.Log("target: " + targetPosition);

        if (Vector3.Distance(playerInstance.transform.position, targetPosition) < 0.001f)
        {
            performingAction = false;
            Debug.Log("DONE!!!");
        }
    }
}
