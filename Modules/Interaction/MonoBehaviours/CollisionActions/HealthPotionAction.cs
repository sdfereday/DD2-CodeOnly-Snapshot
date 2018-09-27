using UnityEngine;

public class HealthPotionAction : MonoBehaviour, IAction
{
    public int PickupValue = 1;

    public void Trigger(Collider other)
    {
        var stats = other.GetComponent<ActorStats>();

        if (stats != null)
        {
            if (stats.HealthMaxed())
                return;

            stats.IncreaseHealth(PickupValue);
            Destroy(gameObject);
        }
    }

    public void Collide(Collision collision) { }
}