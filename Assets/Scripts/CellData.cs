using System;
using System.Collections.Generic;
using UnityEngine;

public class CellData
{
    private bool _topWall, _bottomWall, _leftWall, _rightWall, _isDirty;
    public Vector2Int Position { private set; get; }
    private List<Vector2Int> UnvisitedNeighbours { set; get; }
    private List<Vector2Int> VisitedNeighbours { set; get; }
    public event Action OnDirty;

    public bool IsDirty
    {
        get { return _isDirty; }
    }

    public bool TopWall
    {
        get { return _topWall; }
    }

    public bool BottomWall
    {
        get { return _bottomWall; }
    }

    public bool LeftWall
    {
        get { return _leftWall; }
    }

    public bool RightWall
    {
        get { return _rightWall; }
    }

    public bool HasUnvisitedNeighbour
    {
        get { return UnvisitedNeighbours.Count > 0; }
    }

    public CellData(Vector2Int position)
    {
        Position = position;
        _topWall = true;
        _bottomWall = true;
        _leftWall = true;
        _rightWall = true;
        UnvisitedNeighbours = new List<Vector2Int>();
        VisitedNeighbours = new List<Vector2Int>();
    }

    public void RefreshNeighbours(Dictionary<Vector2Int, CellData> allCells, int mazeWidth, int mazeHeight)
    {
        UnvisitedNeighbours = new List<Vector2Int>();
        VisitedNeighbours = new List<Vector2Int>();

        // left
        if (Position.x > 0)
        {
            var left = new Vector2Int(Position.x - 1, Position.y);

            if (!allCells.ContainsKey(left))
            {
                UnvisitedNeighbours.Add(left);
            }
            else VisitedNeighbours.Add(left);
        }

        // bottom
        if (Position.y > 0)
        {
            var bottom = new Vector2Int(Position.x, Position.y - 1);
            if (!allCells.ContainsKey(bottom))
            {
                UnvisitedNeighbours.Add(bottom);
            }
            else VisitedNeighbours.Add(bottom);
        }

        // right
        if (Position.x < mazeWidth - 1)
        {
            var right = new Vector2Int(Position.x + 1, Position.y);
            if (!allCells.ContainsKey(right))
            {
                UnvisitedNeighbours.Add(right);
            }
            else VisitedNeighbours.Add(right);
        }

        // top
        if (Position.y < mazeHeight - 1)
        {
            var top = new Vector2Int(Position.x, Position.y + 1);
            if (!allCells.ContainsKey(top))
            {
                UnvisitedNeighbours.Add(top);
            }
            else VisitedNeighbours.Add(top);
        }
    }

    public Vector2Int GetRandomNeighbour(System.Random rand)
    {
        return UnvisitedNeighbours[rand.Next(0, UnvisitedNeighbours.Count)];
    }

    public Vector2Int GetRandomVisitedNeighbour(System.Random rand)
    {
        return VisitedNeighbours[rand.Next(0, VisitedNeighbours.Count)];
    }

    public void RemoveWall(Vector2Int dir)
    {
        if (dir.x > Position.x)
            _rightWall = false;
        else if (dir.x < Position.x)
            _leftWall = false;
        else if (dir.y > Position.y)
            _topWall = false;
        else if (dir.y < Position.y)
            _bottomWall = false;
    }

    public void MarkDirty(bool v)
    {
        _isDirty = v;
        if (OnDirty != null) OnDirty.Invoke();
    }
}