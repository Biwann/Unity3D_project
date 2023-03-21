using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{

    [SerializeField] Vector2Int _groundSize;
    [SerializeField] Cell _cellPrefab;
    [SerializeField] float _offset;
    [SerializeField] Transform _cellsParent;

    [SerializeField] GroundRepos _groundRepos;
    [SerializeField] GroundInteractor _groundInteractor;

    public Vector2Int GetGroundSize()
    {
        return _groundSize;
    }

    [ContextMenu("Generate ground")]
    public void GenerateGround()
    {
        ClearChildren(_cellsParent);

        if (_groundRepos && _groundInteractor)
        {
            _groundRepos.Cells.Clear();
            _cellPrefab.GroundInteractor = _groundInteractor;
        }

        var cellSize = _cellPrefab.GetComponent<MeshRenderer>().bounds.size;

        float xSize = _groundSize.x;
        float ySize = _groundSize.y;

        for (int x = 0; x < _groundSize.x; x++)
        {
            for (int y = 0; y < _groundSize.y; y++)
            {
                var position = new Vector3((-xSize / 2f + x) * (cellSize.x + _offset) + cellSize.x / 2f, 0,
                    (-ySize / 2f + y) * (cellSize.z + _offset) + cellSize.z / 2f);

                var cell = Instantiate(_cellPrefab, position, Quaternion.identity, _cellsParent);

                cell.name = "x: " + x + " y: " + y;

                cell.Position = new Vector2(x, y);

                if (_groundRepos)
                {
                    if (_groundRepos.Cells == null)
                        _groundRepos.Cells = new List<Cell>();

                    _groundRepos.Cells.Add(cell);
                }
            }
        }
    }
    void ClearChildren(Transform parent)
    {
        for (int i = 0; i < parent.childCount;)
        {
            DestroyImmediate(parent.GetChild(i).gameObject);
        }
    }

}
