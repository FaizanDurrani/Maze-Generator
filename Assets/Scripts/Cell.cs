using UnityEngine;

public class Cell : MonoBehaviour
{
    private CellData _data;
    [SerializeField] private GameObject _topWall, _bottomWall, _leftWall, _rightWall;

    public void SetData(CellData data)
    {
        _data = data;
        _data.OnDirty += OnDirty;
    }

    private void OnDirty()
    {
        if (_data.IsDirty)
        {
            _topWall.SetActive(_data.TopWall);
            _bottomWall.SetActive(_data.BottomWall);
            _leftWall.SetActive(_data.LeftWall);
            _rightWall.SetActive(_data.RightWall);
            _data.MarkDirty(false);
        }
    }
}