using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [SerializeField] List<Color> _gameColors;
    public List<UnitBase> Units;

    public Color GetColor(int num)
    {
        Color color = Color.white;
        if (_gameColors.Count != 0)
        {
            num = (num) % (_gameColors.Count);
            color = _gameColors[num];
        }
        return color;
    }
    public int GetColorAmount()
    {
        return _gameColors.Count;
    }

    public void ChangeColors(UnitBase moved)
    {
        foreach (var unit in Units)
        {
            if (unit != moved)
            {
                unit.ColorIndex++;
                unit.UpdateColor();
            }
        }
    }

    [ContextMenu("Check win")]
    public bool PlayerWin()
    {
        if (Units.Count > 0)
            foreach (var unit in Units)
            {
                if (!unit.IsWinable())
                {
                    Debug.Log("loose");
                    return false;
                }
            }
        Debug.Log("win");
        return true;
    }
}
