using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum CellType
{
    Floor,
    TargetSpot,
    Wall
}

public class Cell : MonoBehaviour
{
    CellType type;
    Transform item;
}
