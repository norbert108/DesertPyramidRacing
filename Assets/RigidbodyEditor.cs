using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RigidbodyEditor : Editor {

    void OnSceneGUI()
    {
        Rigidbody rb = target as Rigidbody;
        Handles.color = Color.red;
        Handles.SphereCap(1, rb.transform.TransformPoint(rb.centerOfMass), rb.rotation, 1f);
    }
    public override void OnInspectorGUI()
    {
        GUI.skin = EditorGUIUtility.GetBuiltinSkin(UnityEditor.EditorSkin.Inspector);
        DrawDefaultInspector();
    }
}
