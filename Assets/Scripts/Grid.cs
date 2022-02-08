using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [Header("Grid settings")]
    public Vector2 gridSize;

    [Header("Cell settings")]
    public GameObject cellObject;

    private List<Cell> cells;

    private void Start()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                cells.Add(new Cell(x,y));
            }
        }
    }
}
