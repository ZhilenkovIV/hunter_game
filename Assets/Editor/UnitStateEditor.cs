using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(UnitStateMachine))]
public class UnitStateEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.LabelField("Hello, editor");
        UnitStateMachine state = this.target as UnitStateMachine;

    }
}
