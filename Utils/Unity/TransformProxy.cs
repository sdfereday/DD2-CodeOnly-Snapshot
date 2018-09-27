using UnityEngine;

// Gets a specific target transform if being used in a child object
// but you need to access a different one (such as the parent or neighbour).
public class TransformProxy : MonoBehaviour
{
    public Transform TransformTarget;

    private void Start()
    {
        if(TransformTarget == null)
        {
            TransformTarget = transform;
        }
    }
}