// You can turn these in to props, just ensure fields are there for serializing.
[System.Serializable]
public class TileCell
{
    public string id;
    public int x;
    public int y;
    public MAP_CELL_TYPE t;
    public string[] requires;
}

[System.Serializable]
public class EntityModel
{
    public string id;
    public int x;
    public int y;
    public ENTITY_TYPE t;
}

[System.Serializable]
public class LevelDimensions
{
    public int width;
    public int height;
}

[System.Serializable]
public class LevelModel
{
    public string id;
    // public THEME_SLOT themeSlot;
    public LevelDimensions dimensions;
    public TileCell[] floorLayer;
    public EntityModel[] entityLayer;
}