using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoolSO : ScriptableObject
{
    [SerializeField]
    private bool _bool;

    public bool Value
    {
        get { return _bool; }
        set { _bool = value; }
    }

}
