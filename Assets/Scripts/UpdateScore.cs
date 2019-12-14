using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateScore : MonoBehaviour
{
    public TextMeshProUGUI crates;
    public TextMeshProUGUI moves;
    public GameState gameState;


    private void Awake()
    {
        gameState = GameState.Instance;
    }

    void Update()
    {
        crates.text = $"{gameState.PlacedCratesCount}/{gameState.TotalCratesCount} crates";
        moves.text = $"{gameState.PushesCount}/{gameState.TotalMovesCount} moves";
    }
}
