using UnityEngine;

public class Cell
{
    private int x;
    private int y;
    private GameObject cell;

    public Cell(int x, int y, GameObject cell)
    {
        this.x = x;
        this.y = y;
        this.cell = cell;

        spawnCell();
    }

    private void spawnCell()
    {
        cell = GameObject.Instantiate(cell, new Vector2(x, y), Quaternion.identity);
    }

    public void setStart()
    {
        cell.GetComponent<SpriteRenderer>().color = Static.startColor;
    }

    public void setEnd()
    {
        cell.GetComponent<SpriteRenderer>().color = Static.endColor;
    }

    public void setWall()
    {
        cell.GetComponent<SpriteRenderer>().color = Static.wallColor;
    }
}
