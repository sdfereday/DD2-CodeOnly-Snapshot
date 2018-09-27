using UnityEngine;

// Use this if anything needs to happen when a player collides with an enemy,
// or anything for that matter.
public class CollisionWithEnemyAction : MonoBehaviour, IAction
{
    public void Trigger(Collider enemycollider) { }

    public void Collide(Collision collision) { }
}