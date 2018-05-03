using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private Cell _cellPrefab;

    private List<Cell> _unvisitedCells;
    private Cell[] _mazeCells;

    private void Start()
    {
        _mazeCells = new Cell[_width * _height];
        _unvisitedCells = new List<Cell>();

        Vector2Int position = Vector2Int.zero;
        for (int i = 0; i < _mazeCells.Length; i++)
        {
            _mazeCells[i] = Instantiate(_cellPrefab.gameObject, new Vector3(position.x, 0, position.y),
                    Quaternion.identity)
                .GetComponent<Cell>();
            _mazeCells[i].SetPosition(position);
            _mazeCells[i].RefreshNeighbours(_mazeCells, _width, _height);
            _mazeCells[i].MarkVisited(false);

            _unvisitedCells.Add(_mazeCells[i]);

            if (position.x == _width - 1)
            {
                position.x = 0;
                position.y++;
            }
            else position.x++;
        }

        RecursiveCell(_mazeCells[0], null);
    }

    private void RecursiveCell(Cell start, Cell prev)
    {
        var curr = start;
        curr.RefreshNeighbours(_mazeCells, _width, _height);
        while (curr != null)
        {
            _unvisitedCells.Remove(curr);
            curr.MarkVisited(true);

            if (prev != null)
            {
                curr.RemoveWall(prev.Position);
            }

            if (curr.HasUnvisitedNeighbour)
            {
                var next = CellFromPosition(curr.GetRandomNeighbour());
                next.RefreshNeighbours(_mazeCells, _width, _height);
                curr.RemoveWall(next.Position);
                prev = curr;
                curr = next;
            }
            else
            {
                if (_unvisitedCells.Count > 0)
                {
                    var unvisitedCell = _unvisitedCells[0];
                    unvisitedCell.RefreshNeighbours(_mazeCells, _width, _height);
                    prev = CellFromPosition(unvisitedCell.GetRandomVisitedNeighbour());
                    prev.RemoveWall(unvisitedCell.Position);
                    curr = unvisitedCell;
                }
                else break;
            }
        }
    }

    private Cell CellFromPosition(Vector2Int pos)
    {
        return _mazeCells[pos.x + _width * pos.y];
    }
}