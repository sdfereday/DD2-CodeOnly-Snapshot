using System;
using UnityEditor;

[CustomEditor(typeof(LevelLoader))]
public class LevelLoaderEditor : Editor
{
    int _choiceIndex = 0;
    private SerializedProperty currentLevel;

    private void OnEnable()
    {
        currentLevel = serializedObject.FindProperty("CurrentLevel");
        _choiceIndex = Array.IndexOf(LevelListConsts.Levels, currentLevel.stringValue);
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Level To Load:", EditorStyles.boldLabel);
        _choiceIndex = EditorGUILayout.Popup(_choiceIndex, LevelListConsts.Levels);
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        currentLevel.stringValue = LevelListConsts.Levels[_choiceIndex];

        serializedObject.ApplyModifiedProperties();
    }
}