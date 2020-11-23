using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoolValue : ScriptableObject
{

    public bool value;

    private void OnEnable()
    {
        value = true;
    }

}
