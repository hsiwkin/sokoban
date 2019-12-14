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
        if (!performingAction)
        {
            HandleInput();
        }
    }

    private void SetupCamera()
    {
        mainCamera.transform.position = new Vector3(
            gameState.MapSize[1] / 2, // height
            mainCamera.transform.position.y,
            gameState.MapSize[0] / 2 // width
        );
    }

    private void HandleInput()
    {
        if (AnyInput())
        {
            Vector3Int target = GetNewTargetField();
            Move(target);
        }
    }

    private void Move(Vector3Int target)
    {
        performingAction = true;
        
        var playerInstance = gameState
            .mapData[
                gameState.playerPosition[0],
                gameState.playerPosition[1]
            ].item;
        var playerMovementComponent = playerInstance
            .GetComponent<PlayerMovement>();
        UpdatePlayerPosition(target);

        playerMovementComponent.MovementFinish +=
            (object source, System.EventArgs args) => performingAction = false;

        playerMovementComponent.StartMovement(target);
    }

    private Vector3Int GetNewTargetField()
    {
        var verticalInput = Input.GetAxis("Vertical");
        var horizontalInput = Input.GetAxis("Horizontal");

        return new Vector3Int(
            gameState.playerPosition[1] + System.Math.Sign(horizontalInput),
            0,
            gameState.playerPosition[0] + System.Math.Sign(verticalInput)
        );
    }

    private bool AnyInput()
    {
        return System.Math.Abs(Input.GetAxis("Vertical")) > Mathf.Epsilon ||
            Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Epsilon;
    }

    private void UpdatePlayerPosition(Vector3Int target)
    {
        var oldField = gameState
            .mapData[
                gameState.playerPosition[0],
                gameState.playerPosition[1]
            ];

        gameState.playerPosition = new Vector2Int(target.z, target.x);

        var newField = gameState
            .mapData[
                gameState.playerPosition[0],
                gameState.playerPosition[1]
            ];

        
        newField.type = CellType.Player;
        newField.item = oldField.item;

        oldField.type = CellType.Floor;
        oldField.item = null;
    }
}
