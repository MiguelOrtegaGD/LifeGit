using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using System;

[CustomEditor(typeof(GunVisualizer))]
[CanEditMultipleObjects]
public class GunVisualizerEditor : Editor
{
    GunVisualizer gunVis;

    bool set = false;


    void OnEnable()
    {
        gunVis = (GunVisualizer)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        using (new GUILayout.VerticalScope("Manejar Objetos Por Accion", "window", GUILayout.Height(50)))
        {
            using (new GUILayout.VerticalScope((!EditorGUIUtility.isProSkin ? "framebox" : "helpbox")))
            {
                using (new GUILayout.VerticalScope((!EditorGUIUtility.isProSkin ? "framebox" : "helpbox")))
                {
                    if (gunVis.GunObj && !set)
                    {
                        gunVis.GunObj.transform.SetParent(gunVis.transform);
                        gunVis.GunObj.transform.localPosition = Vector3.zero;
                        set = true;
                    }

                    gunVis.Offset = EditorGUILayout.Vector3Field("Offset", gunVis.Offset);
                    gunVis.GunObj = (GameObject)EditorGUILayout.ObjectField("Obj", gunVis.GunObj, typeof(GameObject), true);

                    if (gunVis.GunObj)
                    {
                        gunVis.GunObj.transform.localPosition = Vector3.zero;
                        gunVis.GunObj.transform.localPosition = new Vector3(gunVis.GunObj.transform.localPosition.x + gunVis.Offset.x, gunVis.GunObj.transform.localPosition.y + gunVis.Offset.y, gunVis.GunObj.transform.localPosition.z + gunVis.Offset.z);
                    }
                }
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
