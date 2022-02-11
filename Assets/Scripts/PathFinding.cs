using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public static PathFinding instance;

    private void Awake()
    {
        instance = this;
    }

    public IEnumerator findPath(Cell[,] grid, Vector2Int gridSize, Vector2Int startCell, Vector2Int endCell)
    {
        List<Cell> openList = new List<Cell>() { grid[startCell.x, startCell.y] };
        List<Cell> closedList = new List<Cell>();

        Cell currentCell = openList[0];
        Cell targetCell = grid[endCell.x, endCell.y];

        while(currentCell != targetCell)
        {
            currentCell = openList[0];
            foreach(Cell cell in openList)
            {
                if(cell.fCost < currentCell.fCost || cell.fCost == currentCell.fCost && cell.hCost < currentCell.hCost)
                {
                    currentCell = cell;
                }
            }

            openList.Remove(currentCell);
            closedList.Add(currentCell);

            for (int x = -1 + currentCell.x; x < 2 + currentCell.x; x++)
            {
                for (int y = -1 + currentCell.y; y < 2 + currentCell.y; y++)
                {
                    if (
                        //(x == currentCell.x || y == currentCell.y) &&
                        //(x != currentCell.x || y != currentCell.y) &&
                        (x >= 0 && x < gridSize.x) &&
                        (y >= 0 && y < gridSize.y) &&
                        (closedList.Contains(grid[x,y]) == false) &&
                        (grid[x, y].cellState != Cell.CellState.WALL)
                       )
                    {
                        float newMovementCost = currentCell.gCost + Vector2.Distance(new Vector2(currentCell.x, currentCell.y), new Vector2(grid[x, y].x, grid[x, y].y));

                        if(newMovementCost < grid[x,y].gCost || !openList.Contains(grid[x,y]))
                        {
                            grid[x, y].gCost = newMovementCost;
                            grid[x, y].hCost = Vector2.Distance(new Vector2(grid[x, y].x, grid[x, y].y), endCell);
                            grid[x, y].parent = currentCell;
                            grid[x, y].setWalked();
                            openList.Add(grid[x, y]);
                        }
                        
                    }
                }
            }

            yield return null;
        }

        StartCoroutine(drawPath(grid[startCell.x, startCell.y], grid[endCell.x, endCell.y]));
    }

    public IEnumerator drawPath(Cell startCell, Cell endCell)
    {
        Cell currentCell = endCell;

        while (currentCell != startCell)
        {
            currentCell = currentCell.parent;
            currentCell.setPath();
            yield return null;
        }
    }
}
