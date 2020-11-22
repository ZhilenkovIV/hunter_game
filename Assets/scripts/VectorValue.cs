using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VectorValue : ScriptableObject
{
    public Vector3 initialValue;

    private void OnEnable()
    {
        initialValue = new Vector3(-4.92f, -3.58f, 0);
    }

}
