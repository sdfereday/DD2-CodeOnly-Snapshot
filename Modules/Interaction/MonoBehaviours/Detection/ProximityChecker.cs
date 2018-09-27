using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class ProximityChecker : MonoBehaviour
{
    public CapsuleCollider Capsule;

    private Collider rootCollider { get; set; }
    private Collider currentCollider { get; set; }

    private void Start()
    {
        rootCollider = transform.parent.GetComponent<Collider>();
        currentCollider = GetComponent<Collider>();
    }
    
    public bool OverlapsWithTags(string[] tagsToCheck)
    {
        return PhysicsExtensions.OverlapCapsule(Capsule)
            .Where(x => x != currentCollider && x != rootCollider)
            .ToList()
            .Any(collider => tagsToCheck.Any(tag => tag == collider.tag));
    }

    public List<GameObject> GetCurrentOverlapping()
    {
        return PhysicsExtensions.OverlapCapsule(Capsule)
            .Select(x => x.gameObject)
            .Where(x => x != currentCollider && x != rootCollider)
            .ToList();
    }
}
