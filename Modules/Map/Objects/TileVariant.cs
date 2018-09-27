using UnityEngine;

[System.Serializable]
public class TileVariant
{
    [HideInInspector]
    public int scoreIndex;
    public string name;
    public GameObject prefab;
    public GameObject floorPrefab;
}