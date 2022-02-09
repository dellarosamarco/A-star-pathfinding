using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public static PathFinding instance;
    private Cell lastCell = null;

    private void Awake()
    {
        instance = this;
    }

    public IEnumerator init(Cell[,] grid, Vector2Int gridSize, Vector2Int startCell, Vector2Int endCell)
    {
        Cell currentCell = null;
        Cell bestCell = null;
        float bestHCost = 0;
        while(lastCell != grid[endCell.x, endCell.y]){
            int counter = 0;

            if (lastCell == null)
                currentCell = grid[startCell.x, startCell.y];
            else
                currentCell = lastCell;

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
                        (grid[x, y].cellState != Cell.CellState.WALL))
                    {
                        grid[x, y].setFocus(startCell, endCell);
                        
                        if(counter == 0){
                            bestHCost = grid[x, y].hCost;
                        }

                        if (grid[x, y].hCost <= bestHCost)
                        {
                            bestHCost = grid[x, y].hCost;
                            bestCell = grid[x, y];
                        }

                        counter ++;
                    }
                }
            }

            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    if(grid[x, y].focused)
                    {
                        if(
                           (grid[x, y].cellState != Cell.CellState.WALKED) &&
                           (grid[x, y].cellState != Cell.CellState.WALL)
                           )
                        {
                            if (grid[x, y].hCost < bestHCost)
                            {
                                bestHCost = grid[x, y].hCost;
                                bestCell = grid[x, y];
                            }
                        }
                    }
                }
            }

            bestCell.setWalked();
            lastCell = bestCell;

            yield return new WaitForSeconds(0.05f);
        }
    }
}
