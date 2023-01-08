using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapePool : ObjectPooler
{
    #region Singleton
    public static ShapePool Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion
}
