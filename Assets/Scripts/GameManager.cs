﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject floorBox, crate, wall, targetFloorBox, player;
    public Transform mainCamera;
    private GameState gameState;

    private bool performingAction;

    private void Awake()
    {
        var board1 = "board_1.txt";
        gameState = GameState.Instance;

        new BoardBilder
        {
            floorBox = floorBox,
            crate = crate,
            wall = wall,
            targetFloorBox = targetFloorBox,
            player = player,
            gameManager = this
        }.Run(board1, gameState);
    }

    private void Update()
    {
        if (!performingAction)
        {
            HandleInput();
        }
    }

    private void HandleInput()
    {
        Vector3Int? target = GetNewTargetField();

        if (target.HasValue)
        {
            Trafficer(target.Value);
        }
    }

    private void MovePlayer(Vector3Int target, string activityType)
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

        playerMovementComponent.StartMovement(target, activityType);
    }

    private void MoveCrate(Vector3Int target, Vector3Int nextCellPosition)
    {
        // move crate
        var crateInstance = gameState.mapData[target.z, target.x].item;
        var crateMovement = crateInstance.GetComponent<CrateMovement>();

        crateMovement.StartMovement(new Vector2Int(
            nextCellPosition.z, nextCellPosition.x
        ));

        // update gameStatus with new crate's position
        gameState.mapData[nextCellPosition.z, nextCellPosition.x].item = crateInstance;
        gameState.mapData[nextCellPosition.z, nextCellPosition.x].type = CellType.Crate;

        gameState.mapData[target.z, target.x].item = null;
        gameState.mapData[target.z, target.x].type = CellType.Floor;
    }

    private Vector3Int? GetNewTargetField()
    {
        var verticalInput = Input.GetAxis("Vertical");
        var horizontalInput = Input.GetAxis("Horizontal");

        if (Mathf.Abs(verticalInput) > Mathf.Epsilon)
        {
            return new Vector3Int(
                gameState.playerPosition[1],
                0,
                gameState.playerPosition[0] + System.Math.Sign(verticalInput)
            );
        } else if (Mathf.Abs(horizontalInput) > Mathf.Epsilon)
        {
            return new Vector3Int(
                gameState.playerPosition[1] + System.Math.Sign(horizontalInput),
                0,
                gameState.playerPosition[0] 
             );
        } else
        {
            return null;
        }
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

    private void Trafficer(Vector3Int target)
    {
        var targetType = gameState.mapData[target.z, target.x].type;

        // Free space
        if (targetType == CellType.Floor || targetType == CellType.TargetSpot)
        {
            MovePlayer(target, "walking");
        } else if (targetType == CellType.Crate)
        {
            Vector3Int playerPosition = new Vector3Int(
                gameState.playerPosition[1],
                0,
                gameState.playerPosition[0]
            );

            Vector3Int direction = target - playerPosition;
            Vector3Int nextPosition = target + direction;
            var nextCellType = gameState.mapData[
                nextPosition.z, nextPosition.x].type;

            if (nextCellType == CellType.Floor || nextCellType == CellType.TargetSpot)
            {
                MoveCrate(target, nextPosition);
                MovePlayer(target, "pushing");
            }
        }
    }
}
