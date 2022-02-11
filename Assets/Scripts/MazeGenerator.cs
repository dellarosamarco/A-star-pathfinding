using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public static MazeGenerator instance;

    private void Awake()
    {
        instance = this;
    }

    public IEnumerator generateMaze(Vector2Int gridSize, Cell[,] grid)
    {
        int counter = 0;
        Vector2Int currentPoint = new Vector2Int(Random.Range(0, gridSize.x), Random.Range(0, gridSize.y));

        grid[currentPoint.x, currentPoint.y].setWall();
        
        while(counter < 100)
        {
            counter += 1;

            currentPoint += Static.getDirection(detectCellPosition(currentPoint.x, currentPoint.y, gridSize));

            grid[currentPoint.x, currentPoint.y].setWall();

            yield return null;
        }
    }

    public List<Static.cellPosition> detectCellPosition(int xPos, int yPos, Vector2Int bounds)
    {
        List<Static.cellPosition> positions = new List<Static.cellPosition>();

        if (xPos == 0)
            positions.Add(Static.cellPosition.LEFT);
        else if (xPos == bounds.x-1)
            positions.Add(Static.cellPosition.RIGHT);

        if (yPos == 0)
            positions.Add(Static.cellPosition.BOTTOM);
        if (yPos == bounds.y - 1)
            positions.Add(Static.cellPosition.TOP);

        return positions;
    }
}
