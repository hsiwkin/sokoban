using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateScore : MonoBehaviour
{
    public TextMeshProUGUI crates;
    public TextMeshProUGUI moves;
    public TextMeshProUGUI info;
    public GameState gameState;


    private void Awake()
    {
        gameState = GameState.Instance;
    }

    void Update()
    {
        crates.text = $"{gameState.PlacedCratesCount}/{gameState.TotalCratesCount} crates";
        moves.text = $"{gameState.PushesCount}/{gameState.TotalMovesCount} moves";

        if (gameState.PlacedCratesCount == gameState.TotalCratesCount)
        {
            info.text = "YOU'VE WON! - press ECS to restart";
        }
    }
}
