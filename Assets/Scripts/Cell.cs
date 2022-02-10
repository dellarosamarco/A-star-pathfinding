using UnityEngine;

public class Cell
{
    public int x;
    public int y;
    private Vector2Int pos;
    private GameObject cell;
    private float gCost;
    public float hCost;
    public float fCost;

    public bool focused = false;

    public CellState cellState;
    public enum CellState
    {
        EMPTY,
        START,
        END,
        WALL,
        WALKED
    }

    public Cell(int x, int y, GameObject cell)
    {
        this.x = x;
        this.y = y;
        this.cell = cell;

        pos = new Vector2Int(x, y);

        spawnCell();
    }

    private void spawnCell()
    {
        cell = GameObject.Instantiate(cell, new Vector2(x, y), Quaternion.identity);
        cellState = CellState.EMPTY;
    }

    public void setStart()
    {
        cell.GetComponent<SpriteRenderer>().color = Static.startColor;
        cellState = CellState.START;
    }

    public void setEnd()
    {
        cell.GetComponent<SpriteRenderer>().color = Static.endColor;
        cellState = CellState.END;
    }

    public bool setWall()
    {
        if (cellState == CellState.WALL) return false;

        cell.GetComponent<SpriteRenderer>().color = Static.wallColor;
        cellState = CellState.WALL;

        return true;
    }

    public void setFocus(Vector2Int startCell, Vector2Int endCell)
    {
        cell.GetComponent<SpriteRenderer>().color = Static.focusColor;
        focused = true;

        gCost = Vector2.Distance(startCell, pos);
        hCost = Vector2.Distance(endCell, pos);
        fCost = gCost + hCost;
    }

    public void setWalked()
    {
        cell.GetComponent<SpriteRenderer>().color = Static.walkedColor;
        cellState = CellState.WALKED;
    }
}
