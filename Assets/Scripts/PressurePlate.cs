using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] int _colorNum;
    [SerializeField] Cell _cell;

    private void Start()
    {
        var colorManager = FindObjectOfType<ColorManager>();
        if (colorManager)
            gameObject.GetComponent<MeshRenderer>().material.color =
                colorManager.GetColor(_colorNum);
    }
    private void OnMouseDown()
    {
        if (FindObjectOfType<PauseMenu>().GamePaused == false)
        {
            _cell.ClickCell(_cell);
        }
    }

    [ContextMenu("Init cell")]
    public void InitCell()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (hit.collider.GetComponent<Cell>())
            {
                transform.position = new Vector3(hit.transform.position.x,
                    transform.position.y, hit.transform.position.z);

                var cell = hit.collider.GetComponent<Cell>();

                _cell = cell;
                cell.Plate = this;
            }
        }
    }
    public int GetColorNum()
    {
        return _colorNum;
    }

}
