using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cell : MonoBehaviour
{
    [SerializeField] private GameObject _topWall, _bottomWall, _leftWall, _rightWall;
    public Vector2Int Position { private set; get; }
    public List<Vector2Int> UnvisitedNeighbours { private set; get; }
    public List<Vector2Int> VisitedNeighbours { private set; get; }
    public bool Visited { private set; get; }

    public bool HasUnvisitedNeighbour
    {
        get { return UnvisitedNeighbours.Count > 0; }
    }


    public void MarkVisited(bool v)
    {
        Visited = v;
    }

    public void SetPosition(Vector2Int position)
    {
        Position = position;
    }

    public void RefreshNeighbours(Cell[] allCells, int mazeWidth, int mazeHeight)
    {
        UnvisitedNeighbours = new List<Vector2Int>();
        VisitedNeighbours = new List<Vector2Int>();
        
        // left
        if (Position.x > 0)
        {
            var left = new Vector2Int(Position.x - 1, Position.y);

            if (allCells[left.x + mazeWidth * left.y] == null || !allCells[left.x + mazeWidth * left.y].Visited)
            {
                UnvisitedNeighbours.Add(left);
            }else VisitedNeighbours.Add(left);
        }

        // bottom
        if (Position.y > 0)
        {
            var bottom = new Vector2Int(Position.x, Position.y - 1);
            if (allCells[bottom.x + mazeWidth * bottom.y] == null || !allCells[bottom.x + mazeWidth * bottom.y].Visited)
            {
                UnvisitedNeighbours.Add(bottom);
            }else VisitedNeighbours.Add(bottom);
        }

        // right
        if (Position.x < mazeWidth - 1)
        {
            var right = new Vector2Int(Position.x + 1, Position.y);
            if (allCells[right.x + mazeWidth * right.y] == null || !allCells[right.x + mazeWidth * right.y].Visited)
            {
                UnvisitedNeighbours.Add(right);
            }else VisitedNeighbours.Add(right);
        }

        // top
        if (Position.y < mazeHeight - 1)
        {
            var top = new Vector2Int(Position.x, Position.y + 1);
            if (allCells[top.x + mazeWidth * top.y] == null || !allCells[top.x + mazeWidth * top.y].Visited)
            {
                UnvisitedNeighbours.Add(top);
            }else VisitedNeighbours.Add(top);
        }
    }

    public Vector2Int GetRandomNeighbour()
    {
        return UnvisitedNeighbours[Random.Range(0, UnvisitedNeighbours.Count)];
    }

    public Vector2Int GetRandomVisitedNeighbour()
    {
        return VisitedNeighbours[Random.Range(0, VisitedNeighbours.Count)];
    }

    public void RemoveWall(Vector2Int dir)
    {
        if (dir.x > Position.x)
            _rightWall.SetActive(false);
        else if (dir.x < Position.x)
            _leftWall.SetActive(false);
        else if (dir.y > Position.y)
            _topWall.SetActive(false);
        else if (dir.y < Position.y)
            _bottomWall.SetActive(false);
    }
}