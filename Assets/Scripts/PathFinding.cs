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

    public IEnumerator init(Cell[,] grid, Vector2Int gridSize, Vector2Int startCell, Vector2Int endCell)
    {
        Cell currentCell = grid[startCell.x, startCell.y];
        Cell targetCell = grid[endCell.x, endCell.y];
        List<Cell> focusedCells = new List<Cell>();

        while(currentCell != targetCell)
        {
            for (int x = -1 + currentCell.x; x < 2 + currentCell.x; x++)
            {
                for (int y = -1 + currentCell.y; y < 2 + currentCell.y; y++)
                {
                    if (
                        (x == currentCell.x || y == currentCell.y) &&
                        (x != currentCell.x || y != currentCell.y) &&
                        (x >= 0 && x < gridSize.x) &&
                        (y >= 0 && y < gridSize.y) &&
                        (grid[x, y].cellState != Cell.CellState.WALKED) &&
                        (grid[x, y].cellState != Cell.CellState.WALL) &&
                        (grid[x, y].focused == false))
                    {
                        grid[x, y].setFocus(startCell, endCell);
                        focusedCells.Add(grid[x, y]);
                    }
                }
            }

            Cell bestFocusedCell = focusedCells[0];
            int priority = 3;
            foreach (Cell cell in focusedCells)
            {
                if(cell.fCost < bestFocusedCell.fCost && cell.hCost < bestFocusedCell.hCost && priority >= 0)
                {
                    bestFocusedCell = cell;
                    priority = 0;
                }
                else if (cell.fCost < bestFocusedCell.fCost && priority >= 1)
                {
                    bestFocusedCell = cell;
                    priority = 1;
                }
                else if(cell.fCost == bestFocusedCell.fCost && cell.hCost < bestFocusedCell.hCost && priority >= 2)
                {
                    bestFocusedCell = cell;
                    priority = 2;
                }
                else if(cell.fCost == bestFocusedCell.fCost && priority >= 3)
                {
                    bestFocusedCell = cell;
                    priority = 3;
                }
            }
            focusedCells.Remove(bestFocusedCell);
            currentCell = bestFocusedCell;
            bestFocusedCell.setWalked();

            yield return null;
        }
    }


}
