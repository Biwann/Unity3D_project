using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidUnit : UnitBase
{
    private void Start()
    {
        MoveVariants.Clear();
        MoveVariants.Add(new Vector2(0, 0));
    }
}
