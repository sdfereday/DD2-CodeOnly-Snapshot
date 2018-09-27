using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Mapper : MonoBehaviour
{
    public class MapResult
    {
        public VirtualEntity StartPoint;
        public VirtualEntity EndPoint;
        public List<GameObject> InstanceCache;
    }

    public delegate void MapGeneratedAction(MapResult Result);
    public static event MapGeneratedAction OnMapGenerated;

    public Transform MapRoot;
    public TileVariant[] TileVariantMap;
    public EntityVariant[] EntityVariantMap;

    // Testing loading via json
    private LevelLoader LevelLoaderComponent;
    private List<VirtualEntity> VirtualEntityCache;
    private List<GameObject> InstantiatedEntityCache;
    private List<VirtualTile> TileCache;
   
    private List<VirtualTile> GenerateTileCache(TileCell[] map)
    {
        List<VirtualTile> Cache = new List<VirtualTile>();
        for (int i = map.Length - 1; i >= 0; i--)
        {
            TileCell currentTile = map[i];
            TileCell northTile = map.FirstOrDefault(tile => tile.x == currentTile.x && tile.y == currentTile.y - 1);
            TileCell southTile = map.FirstOrDefault(tile => tile.x == currentTile.x && tile.y == currentTile.y + 1);
            TileCell eastTile = map.FirstOrDefault(tile => tile.x == currentTile.x + 1 && tile.y == currentTile.y);
            TileCell westTile = map.FirstOrDefault(tile => tile.x == currentTile.x - 1 && tile.y == currentTile.y);

            MAP_CELL_TYPE n = northTile == null || northTile.t == MAP_CELL_TYPE.BLOCKED ? MAP_CELL_TYPE.BLOCKED : MAP_CELL_TYPE.WALKABLE;
            MAP_CELL_TYPE s = southTile == null || southTile.t == MAP_CELL_TYPE.BLOCKED ? MAP_CELL_TYPE.BLOCKED : MAP_CELL_TYPE.WALKABLE;
            MAP_CELL_TYPE e = eastTile == null || eastTile.t == MAP_CELL_TYPE.BLOCKED ? MAP_CELL_TYPE.BLOCKED : MAP_CELL_TYPE.WALKABLE;
            MAP_CELL_TYPE w = westTile == null || westTile.t == MAP_CELL_TYPE.BLOCKED ? MAP_CELL_TYPE.BLOCKED : MAP_CELL_TYPE.WALKABLE;

            VirtualTile vTile = new VirtualTile()
            {
                x = currentTile.x,
                y = currentTile.y,
                type = currentTile.t,
                score = Helpers.GetTileScore(
                    n != MAP_CELL_TYPE.BLOCKED,
                    e != MAP_CELL_TYPE.BLOCKED,
                    s != MAP_CELL_TYPE.BLOCKED,
                    w != MAP_CELL_TYPE.BLOCKED
                )
            };

            Cache.Add(vTile);
        }

        return Cache;
    }

    private List<VirtualEntity> GenerateEntityCache(EntityModel[] map)
    {
        List<VirtualEntity> Cache = new List<VirtualEntity>();
        for (int i = map.Length - 1; i >= 0; i--)
        {
            EntityModel ent = map[i];
            Cache.Add(new VirtualEntity()
            {
                id = ent.id,
                x = ent.x,
                y = ent.y,
                type = ent.t
            });
        }
        return Cache;
    }

    private void OnEnable()
    {
        LevelLoader.OnMapDataLoaded += GenerateLevel;
    }

    private void OnDisable()
    {
        LevelLoader.OnMapDataLoaded -= GenerateLevel;
    }

    private void Start()
    {
        LevelLoaderComponent = GetComponent<LevelLoader>();
        InstantiatedEntityCache = new List<GameObject>();
    }

    private void GenerateLevel(LevelModel[] levels)
    {
        TileCache = GenerateTileCache(levels.FirstOrDefault().floorLayer);
        VirtualEntityCache = GenerateEntityCache(levels.FirstOrDefault().entityLayer);

        if (MapRoot == null)
        {
            MapRoot = transform;
        }

        Debug.Log("Generating map and entities...");

        TileCache.ForEach(tile =>
        {
            // TODO: Initially add a type of decor for the asset to use on its decor slot. This will just instantiate over the top of the
            // wall (or floor). I was going to be cleverer about this but frankly for now it'll be fine as is.
            // Map editor will require a new update and type attached.
            TileVariant v = TileVariantMap.FirstOrDefault(x => x.scoreIndex == tile.score);

            if (v.prefab != null)
            {
                GameObject prefabType = tile.type == MAP_CELL_TYPE.BLOCKED ? v.prefab : v.floorPrefab;
                Vector3 ScaledPosition = new Vector3(
                    tile.x,
                    transform.position.y,
                    tile.y
                );
                var inst = Instantiate(prefabType, ScaledPosition, Quaternion.identity);
                inst.transform.SetParent(MapRoot);

                // For Debug
                inst.name = "TS: " + v.scoreIndex + " [x" + tile.x + "]:[y" + tile.y + "]";
            }
        });
        
        VirtualEntityCache.ForEach(entity =>
        {
            EntityVariant v = EntityVariantMap.FirstOrDefault(x => x.type == entity.type);

            if (v != null && v.prefab != null)
            {
                Vector3 ScaledPosition = new Vector3(
                    entity.x, // TODO: I don't understand why these must be flipped... research needed
                    transform.position.y, // Temporary until I fix the prefabs
                    entity.y
                );

                // This provides a link between the actual prefab in the game world and how it's interpreted
                // when the player hits in within map data (such as passage checking).
                var inst = Instantiate(v.prefab, ScaledPosition, Quaternion.identity);
                inst.transform.SetParent(MapRoot);
                InstantiatedEntityCache.Add(inst);
            }
        });

        if (OnMapGenerated != null)
        {
            OnMapGenerated(new MapResult()
            {
                StartPoint = VirtualEntityCache.FirstOrDefault(x => x.type == ENTITY_TYPE.START_POINT),
                EndPoint = VirtualEntityCache.FirstOrDefault(x => x.type == ENTITY_TYPE.END_POINT),
                InstanceCache = InstantiatedEntityCache
            });
        }
    }

    public VirtualTile GetTileByPosition(int x, int y)
    {
        return TileCache.FirstOrDefault(tile => tile.x == x && tile.y == y);
    }
    
    public bool WalkableAt(int x, int y)
    {
        return TileCache.Any(tile => tile.x == x && tile.y == y && tile.type == MAP_CELL_TYPE.WALKABLE);
    }
}