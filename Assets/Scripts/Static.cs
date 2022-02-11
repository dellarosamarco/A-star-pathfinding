using UnityEngine;
using System.Collections.Generic;

public static class Static
{
    public static Color emptyColor = new Color(1f, 1f, 1f, 1f);
    public static Color startColor = new Color(1f, 0.25f, 0.25f, 1f);
    public static Color endColor = new Color(0.25f, 0.4f, 1f, 1f);
    public static Color wallColor = new Color(0f, 0f, 0f, 1f);
    public static Color focusColor = new Color(0f, 0.8f, 0f, 1f);
    public static Color walkedColor = new Color(0.5f, 0.75f, 0.75f, 1f);
    public static Color pathColor = new Color(0f, 0f, 1f, 1f);

    public static Vector2Int top = new Vector2Int(0, 1);
    public static Vector2Int bottom = new Vector2Int(0, -1);
    public static Vector2Int right = new Vector2Int(1, 0);
    public static Vector2Int left = new Vector2Int(-1, 0);

    public static Vector2Int getDirection(List<cellPosition> positions)
    {
        List<Vector2Int> directions = new List<Vector2Int>();
        if (positions.Count == 0) 
        {
            directions = new List<Vector2Int>() { top, bottom, right, left };
        }
        else
        {
            for (int i = 0; i < positions.Count; i++)
            {
                directions.Add(typeToDir(positions[i]));
            }
        }

        return directions[Random.Range(0, directions.Count)];
    }

    private static Vector2Int typeToDir(cellPosition pos)
    {
        switch (pos)
        {
            case cellPosition.TOP:
                return bottom;
            case cellPosition.BOTTOM:
                return top;
            case cellPosition.RIGHT:
                return left;
            case cellPosition.LEFT:
                return right;
            default:
                return Vector2Int.zero;
        }
    }

    public enum cellPosition
    {
        ALL,
        TOP,
        BOTTOM,
        RIGHT,
        LEFT
    }
}
