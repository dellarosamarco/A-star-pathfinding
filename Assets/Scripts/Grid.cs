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

    private Vector2Int startCellIndex;
    private Vector2Int endCellIndex;
    private List<Vector2Int> wallsIndex = new List<Vector2Int>();

    private bool searchingPath = false;

    private void Start()
    {
        cameraTransform.position = new Vector3(gridSize.x / 2, gridSize.y / 2, -10);
        cameraTransform.gameObject.GetComponent<Camera>().orthographicSize = gridSize.x / 1.666f;

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
        endCellIndex = new Vector2Int(Random.Range(0, gridSize.x), Random.Range(0, gridSize.y));
        grid[endCellIndex.x, endCellIndex.y].setEnd();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = (int)(worldPosition.x + 0.5f);
            int y = (int)(worldPosition.y + 0.5f);

            if (x < 0 || y < 0 || x >= gridSize.x || y >= gridSize.y ||
                grid[x,y].cellState == Cell.CellState.START ||grid[x, y].cellState == Cell.CellState.END)
                return;

            if(grid[x, y].setWall())
            {
                wallsIndex.Add(new Vector2Int(x, y));
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (searchingPath)
            {
                foreach(Cell cell in cells)
                {
                    cell.reset();
                }
            }

            searchingPath = true;

            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = (int)(worldPosition.x + 0.5f);
            int y = (int)(worldPosition.y + 0.5f);

            if (x < 0 || y < 0 || x >= gridSize.x || y >= gridSize.y || 
                grid[x, y].cellState == Cell.CellState.END)
                return;

            grid[x, y].setStart();
            startCellIndex = new Vector2Int(x, y);

            StartCoroutine(PathFinding.instance.findPath(grid, gridSize, startCellIndex, endCellIndex));
        }
    }
}
