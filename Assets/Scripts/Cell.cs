using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum CellState
{
    Standart,
    Selected,
    Variant
}
public class Cell : MonoBehaviour
{
    public Color StandartColor;
    public Color OnMouseEnterColor;
    public Color OnMouseMoveColor;

    public Color SelectedColor;
    public Color MoveColor;

    public UnitBase Unit;
    public PressurePlate Plate;

    public Vector2 Position;

    [HideInInspector] public CellState CellState;

    public GroundInteractor GroundInteractor;

    [SerializeField] bool _winCondition;


    private void Start()
    {
        ChangeColor(StandartColor);
    }

    public void ClickCell(Cell cell)
    {
        if (cell.CellState == CellState.Selected)
        {
            GroundInteractor.DeselectCell(cell);
        }
        else if (cell.CellState == CellState.Variant)
        {
            GroundInteractor.GroundRepos.SelectedCell.Unit.MoveToCell(cell);
        }
        else if (cell.CellState == CellState.Standart)
        {
            if (GroundInteractor.GroundRepos.SelectedCell)
            {
                GroundInteractor.DeselectCell(cell.GroundInteractor.GroundRepos.SelectedCell);
            }

            GroundInteractor.SelectedCell(cell);
        }
    }
    private void OnMouseDown()
    {
        if (FindObjectOfType<PauseMenu>().GamePaused == false)
            ClickCell(this); 
    }

    private void OnMouseEnter()
    {
        if (FindObjectOfType<PauseMenu>().GamePaused == false)
        {
            if (CellState == CellState.Standart)
                ChangeColor(OnMouseEnterColor);
            else if (CellState == CellState.Variant)
                ChangeColor(OnMouseMoveColor);
            else if (CellState == CellState.Selected)
                ChangeColor(SelectedColor);
        }
    }

    private void OnMouseExit()
    {
        if (FindObjectOfType<PauseMenu>().GamePaused == false)
        {
            if (CellState == CellState.Standart)
                ChangeColor(StandartColor);
            else if (CellState == CellState.Selected)
                ChangeColor(SelectedColor);
            else if (CellState == CellState.Variant)
                ChangeColor(MoveColor);
        }
    }

    public void ChangeColor(Color color)
    {
        var mesh = gameObject.GetComponent<MeshRenderer>();
        if (mesh)
            mesh.material.color = color;
    }
}
