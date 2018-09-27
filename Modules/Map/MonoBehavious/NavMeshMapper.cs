using UnityEngine;
using UnityEngine.AI;

public class NavMeshMapper : MonoBehaviour
{
    public NavMeshSurface surface;

    private void OnEnable()
    {
        Mapper.OnMapGenerated += Build;
    }

    private void OnDisable()
    {
        Mapper.OnMapGenerated -= Build;
    }

    private void Build(Mapper.MapResult result)
    {
        surface.BuildNavMesh();
    }
}
