using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(Mapper))]
public class MapperEditor : Editor
{
    private const int MaxScoreVariations = 15;
    private SerializedProperty mapRoot;
    private SerializedProperty startPointPrefab;
    private SerializedProperty spx;
    private SerializedProperty spy;
    private SerializedProperty tileVariantList;
    private SerializedProperty entityVariantList;
    private GUIStyle horizontalLine;
    
    private void HorizontalLine(Color color)
    {
        var c = GUI.color;
        GUI.color = color;
        GUILayout.Box(GUIContent.none, horizontalLine);
        GUI.color = c;
    }

    private List<SerializedProperty> GetVariantsByScores(int[] score)
    {
        List<SerializedProperty> props = new List<SerializedProperty>();

        for (int i = 0; i <= MaxScoreVariations; i++)
        {
            if (score.Any(sc => sc == i))
            {
                var t = tileVariantList.GetArrayElementAtIndex(i);
                SerializedProperty tScore = t.FindPropertyRelative("scoreIndex");
                tScore.intValue = i;

                props.Add(t);
            }
        }

        return props;
    }

    private void ConstructInterface(List<SerializedProperty> props)
    {
        props.ForEach(t => {
            SerializedProperty MyStr = t.FindPropertyRelative("name");
            SerializedProperty MyGO = t.FindPropertyRelative("prefab");
            SerializedProperty MyFLOOR = t.FindPropertyRelative("floorPrefab");
            SerializedProperty MyInt = t.FindPropertyRelative("scoreIndex");
            
            if (MyStr.stringValue == string.Empty)
            {
                MyStr.stringValue = "variant-" + MyInt.intValue;
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Name: " + MyStr.stringValue);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(MyGO, new GUIContent("Score " + MyInt.intValue + " Prefab:"));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(MyFLOOR, new GUIContent("Score " + MyInt.intValue + " Floor Prefab:"));
            EditorGUILayout.EndHorizontal();
        });

        EditorGUILayout.Space();
        EditorGUILayout.Space();
    }

    private void OnEnable()
    {
        mapRoot = serializedObject.FindProperty("MapRoot");
        startPointPrefab = serializedObject.FindProperty("StartPointPrefab");
        spx = serializedObject.FindProperty("StartPointX");
        spy = serializedObject.FindProperty("StartPointY");
        tileVariantList = serializedObject.FindProperty("TileVariantMap");
        entityVariantList = serializedObject.FindProperty("EntityVariantMap");
        horizontalLine = new GUIStyle();
        horizontalLine.normal.background = EditorGUIUtility.whiteTexture;
        horizontalLine.margin = new RectOffset(0, 0, 4, 4);
        horizontalLine.fixedHeight = 1;
    }

    public override void OnInspectorGUI()
    {
        if (tileVariantList.arraySize == 0)
        {
            for (int i = 0; i <= MaxScoreVariations; i++)
            {
                tileVariantList.InsertArrayElementAtIndex(i);
            }
        }

        var cardinals = GetVariantsByScores(new int[] { 1, 2, 4, 8 });
        var corners = GetVariantsByScores(new int[] { 3, 6, 9, 12 });
        var ends = GetVariantsByScores(new int[] { 7, 11, 13, 14 });
        var spines = GetVariantsByScores(new int[] { 5, 10 });
        var pillars = GetVariantsByScores(new int[] { 15 });
        var voided = GetVariantsByScores(new int[] { 0 });

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Map Config:", EditorStyles.boldLabel);
        HorizontalLine(Color.grey);

        EditorGUILayout.PropertyField(mapRoot, new GUIContent("Map Root:"));
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Cardinal Variant Prefabs:", EditorStyles.boldLabel);
        HorizontalLine(Color.grey);
        ConstructInterface(cardinals);

        EditorGUILayout.LabelField("Corner Variant Prefabs:", EditorStyles.boldLabel);
        HorizontalLine(Color.grey);
        ConstructInterface(corners);

        EditorGUILayout.LabelField("End Cap Variant Prefabs:", EditorStyles.boldLabel);
        HorizontalLine(Color.grey);
        ConstructInterface(ends);

        EditorGUILayout.LabelField("Spine Variant Prefabs:", EditorStyles.boldLabel);
        HorizontalLine(Color.grey);
        ConstructInterface(spines);

        EditorGUILayout.LabelField("Pillar Variant Prefabs:", EditorStyles.boldLabel);
        HorizontalLine(Color.grey);
        ConstructInterface(pillars);

        EditorGUILayout.LabelField("Void Variant Prefabs:", EditorStyles.boldLabel);
        HorizontalLine(Color.grey);
        ConstructInterface(voided);

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Entity Map:", EditorStyles.boldLabel);
        HorizontalLine(Color.grey);

        if (entityVariantList.arraySize > 0) {
            for (int i = 0; i < entityVariantList.arraySize; i++)
            {
                var t = entityVariantList.GetArrayElementAtIndex(i);
                SerializedProperty prefab = t.FindPropertyRelative("prefab");
                SerializedProperty Type = t.FindPropertyRelative("type");

                EditorGUILayout.PropertyField(prefab, new GUIContent("Swawn prefab:"));
                ENTITY_TYPE selectedType = (ENTITY_TYPE)EditorGUILayout.EnumPopup("When type is:", (ENTITY_TYPE)Type.enumValueIndex);
                Type.enumValueIndex = (int)selectedType;
            }
        }

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Item +"))
        {
            entityVariantList.InsertArrayElementAtIndex(entityVariantList.arraySize);
        }
        if (GUILayout.Button("Remove Item -") && entityVariantList.arraySize > 1)
        {
            entityVariantList.DeleteArrayElementAtIndex(entityVariantList.arraySize - 1);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        
        serializedObject.ApplyModifiedProperties();        
    }

}