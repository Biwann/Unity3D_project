using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GroundInteractor : MonoBehaviour
{
    public GroundRepos GroundRepos;

    public void SelectedCell(Cell cell)
    {
        GroundRepos.SelectedCell = cell;
        cell.CellState = CellState.Selected;
        cell.ChangeColor(cell.SelectedColor);

        if (cell.Unit != null)
        {
            cell.Unit.Variants = CalculateVariants(cell, cell.Unit.MoveVariants);

            ShowVariants(cell.Unit.Variants);
        }
    }

    public void DeselectCell(Cell cell)
    {
        var selectedCell = GroundRepos.SelectedCell;
        selectedCell.CellState = CellState.Standart;
        selectedCell.ChangeColor(selectedCell.StandartColor);
        GroundRepos.SelectedCell = null;

        if (cell.Unit != null)
        {
            ClearVariants(cell.Unit.Variants);
            cell.Unit.Variants.Clear();
        }
        
    }

    public void ShowVariants(List<Cell> variants)
    {
        foreach (var variant in variants)
        {
            variant.CellState = CellState.Variant;
            variant.ChangeColor(variant.MoveColor);
        }
    }

    public List<Cell> CalculateVariants(Cell unitCell, List<Vector2> moveVariants)
    {
        var variants = new List<Cell>();
        Cell cell;

        foreach (var offset in moveVariants)
        {
            cell = FindCellByPosition(offset, unitCell);
            if (cell)
            {
                if (cell.Unit == null)
                    variants.Add(cell);
            }
        }

        return variants;
    }

    public Cell FindCellByPosition(Vector2 offset, Cell unitCell)
    {
        Vector2 cellNeedPosition = unitCell.Position + offset;
        return GroundRepos.Cells.Find(cell => cell.Position == cellNeedPosition && cell.Unit == null);
    }

    public void ClearVariants(List<Cell> variants)
    {
        foreach (var variant in variants)
        {
            variant.CellState = CellState.Standart;
            variant.ChangeColor(variant.StandartColor);
        }
    }
}
