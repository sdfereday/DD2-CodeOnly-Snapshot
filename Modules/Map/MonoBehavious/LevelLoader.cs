using System.Collections;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [HideInInspector]
    public string CurrentLevel;

    public static LevelLoader instance = null;
    public delegate void MapDataLoaded(LevelModel[] levels);
    public static event MapDataLoaded OnMapDataLoaded;
    
    private LevelModel[] levels;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // Keep an eye on this stuff (async loading).
        StartCoroutine(loadFromResourcesFolder());

        // Sets this to not be destroyed when reloading scene (is this needed if its on persistant scene?)
        //DontDestroyOnLoad(gameObject);
    }

    IEnumerator loadFromResourcesFolder()
    {
        // Request data to be loaded
        ResourceRequest loadAsync = Resources.LoadAsync(CurrentLevel);

        //Wait till we are done loading
        while (!loadAsync.isDone)
        {
            yield return null;
        }

        //Get the loaded data
        TextAsset asset = loadAsync.asset as TextAsset;
        levels = JsonHelper.FromJson<LevelModel>(asset.text);

        if (levels.Length > 0 && OnMapDataLoaded != null)
        {
            OnMapDataLoaded(levels);
        }
    }

    public LevelModel[] GetAllLevels()
    {
        return levels;
    }

    public LevelModel GetLevelAfter(int index)
    {
        if (index > levels.Length - 1)
        {
            throw new UnityException("GetLevelAfter: Index was greater than levels available.");
        }

        return levels[index];
    }

    public bool AlreadyLoaded()
    {
        return levels != null && levels.Length > 0;
    }
}