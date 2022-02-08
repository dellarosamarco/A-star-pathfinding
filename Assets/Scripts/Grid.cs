using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [Header("Grid settings")]
    public Vector2Int gridSize;

    [Header("Cell settings")]
    public GameObject cellObject;

    [Header("Components")]
    public Transform cameraTransform;

    private List<Cell> cells = new List<Cell>();
    private Cell[,] grid;

    private int startCellIndex;
    private int endCellIndex;

    private void Start()
    {
        cameraTransform.position = new Vector3(gridSize.x / 2, gridSize.y / 2, -10);
        cameraTransform.gameObject.GetComponent<Camera>().orthographicSize = 15;

        grid = new Cell[gridSize.x, gridSize.y];

        //Create grid
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Cell newCell = new Cell(x, y, cellObject);
                cells.Add(newCell);
                grid[x, y] = newCell;
            }
        }

        //Set starting point
        startCellIndex = Random.Range(0, cells.Count);
        endCellIndex = Random.Range(0, cells.Count);

        while(endCellIndex == startCellIndex)
            endCellIndex = Random.Range(0, cells.Count);

        cells[startCellIndex].setStart();
        cells[endCellIndex].setEnd();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = (int)(worldPosition.x + 0.5f);
            int y = (int)(worldPosition.y + 0.5f);
            grid[x, y].setWall();
        }
    }
}
