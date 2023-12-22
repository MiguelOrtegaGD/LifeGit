using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using System;

[CustomEditor(typeof(HandleObjectsWithAction))]
[CanEditMultipleObjects]
public class HandleObjectsWithActionEditor : Editor
{
    HandleObjectsWithAction handle;

    SerializedProperty activationType;
    SerializedProperty startEvents;
    SerializedProperty finishEvents;
    SerializedProperty cantFinishEvents;
    SerializedProperty key;
    SerializedProperty tags;
    SerializedProperty delay;
    SerializedProperty trigger;


    void OnEnable()
    {
        handle = (HandleObjectsWithAction)target;

        activationType = serializedObject.FindProperty("activationType");
        startEvents = serializedObject.FindProperty("eventosIniciales");
        finishEvents = serializedObject.FindProperty("eventosFinales");
        cantFinishEvents = serializedObject.FindProperty("eventosNoCumplidasValidaciones");
        key = serializedObject.FindProperty("key");
        tags = serializedObject.FindProperty("tags");
        delay = serializedObject.FindProperty("delay");
        trigger = serializedObject.FindProperty("trigger");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        ActivationType enumValue = (ActivationType)activationType.enumValueIndex;

        using (new GUILayout.VerticalScope("Manejar Objetos Por Accion", "window", GUILayout.Height(50)))
        {
            using (new GUILayout.VerticalScope((!EditorGUIUtility.isProSkin ? "framebox" : "helpbox")))
            {
                using (new GUILayout.VerticalScope((!EditorGUIUtility.isProSkin ? "framebox" : "helpbox")))
                {
                    EditorGUILayout.PropertyField(delay, new GUIContent("Tiempo activación:"));

                    EditorGUILayout.PropertyField(activationType, new GUIContent("Tipo activación:"));

                    if (enumValue == ActivationType.CollisionAndKey || enumValue == ActivationType.Key)
                        EditorGUILayout.PropertyField(key);

                    if (enumValue == ActivationType.Collision || enumValue == ActivationType.CollisionAndKey)
                    {
                        EditorGUILayout.PropertyField(tags);
                        EditorGUILayout.PropertyField(trigger);
                    }
                }

                if (handle.Validations.Count > 0)
                {
                    using (new GUILayout.VerticalScope((!EditorGUIUtility.isProSkin ? "framebox" : "helpbox")))
                    {
                        EditorGUILayout.Space(15);

                        for (int i = 0; i < handle.Validations.Count; i++)
                        {
                            EditorGUILayout.BeginHorizontal();
                            {
                                EditorGUILayout.BeginVertical("HelpBox");
                                {
                                    handle.Validations[i].ValidationType = (ValidationEnum)EditorGUILayout.EnumPopup(handle.Validations[i].ValidationType);

                                    switch (handle.Validations[i].ValidationType)
                                    {
                                        //case ValidationEnum.Score:
                                        //    EditorGUILayout.BeginHorizontal();
                                        //    {
                                        //        EditorGUIUtility.labelWidth = 70;
                                        //        handle.Validations[i].ScoreType = (TypeScore)EditorGUILayout.EnumPopup("Puntuacion:", handle.Validations[i].ScoreType);
                                        //        handle.Validations[i].NumericValidationType = (NumericValidationEnum)EditorGUILayout.EnumPopup(handle.Validations[i].NumericValidationType);
                                        //        handle.Validations[i].IntValue = EditorGUILayout.IntField(handle.Validations[i].IntValue);
                                        //    }
                                        //    EditorGUILayout.EndHorizontal();
                                        //    break;

                                        //case ValidationEnum.SceneName:
                                        //    handle.Validations[i].StringValue = EditorGUILayout.TextArea(handle.Validations[i].StringValue);
                                        //    break;

                                        //case ValidationEnum.Level:
                                        //    EditorGUIUtility.labelWidth = 110;
                                        //    handle.Validations[i].StringValue = EditorGUILayout.TextField("Nombre escena:", handle.Validations[i].StringValue);
                                        //    break;

                                        case ValidationEnum.PlayerPrefs:
                                            EditorGUILayout.BeginHorizontal();
                                            {
                                                EditorGUIUtility.labelWidth = 100;
                                                handle.Validations[i].BoolValue = EditorGUILayout.Toggle("Escena actual:", handle.Validations[i].BoolValue/*, GUILayout.Width(50)*/);
                                                EditorGUIUtility.labelWidth = 50;
                                                if (!handle.Validations[i].BoolValue)
                                                    handle.Validations[i].StringValue = EditorGUILayout.TextField("Valor:", handle.Validations[i].StringValue);
                                            }
                                            EditorGUILayout.EndHorizontal();
                                            break;

                                        //case ValidationEnum.Question:
                                        //    EditorGUIUtility.labelWidth = 110;
                                        //    handle.Validations[i].StringValue = EditorGUILayout.TextField("Nombre escena:", handle.Validations[i].StringValue);
                                        //    break;
                                    }
                                }
                                EditorGUILayout.EndVertical();

                                if (GUILayout.Button("X"))
                                {
                                    handle.Validations.RemoveAt(i);
                                }
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                    }
                }

                if (GUILayout.Button("Agregar condicion"))
                {
                    handle.Validations.Add(new Validation());
                }

                using (new GUILayout.VerticalScope((!EditorGUIUtility.isProSkin ? "framebox" : "helpbox")))
                {
                    EditorGUILayout.LabelField("Eventos realizados en el start", EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(startEvents);
                }

                using (new GUILayout.VerticalScope((!EditorGUIUtility.isProSkin ? "framebox" : "helpbox")))
                {
                    EditorGUILayout.LabelField("Eventos realizados al cumplir las condiciones", EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(finishEvents);
                }

                using (new GUILayout.VerticalScope((!EditorGUIUtility.isProSkin ? "framebox" : "helpbox")))
                {
                    EditorGUILayout.LabelField("Eventos realizados si no se cumplen las condiciones", EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(cantFinishEvents);
                }
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
