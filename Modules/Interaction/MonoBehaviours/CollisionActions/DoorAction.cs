using UnityEngine;

public class DoorAction : MonoBehaviour, IAction, IDoor
{
    public bool IsLocked { get; private set; }
    public int RequiresKeyId;

    private void Start()
    {
        IsLocked = true;
    }

    public void Trigger(Collider other)
    {
        var inventory = other.GetComponentInParent<PlayerInventory>();
        if (inventory != null && inventory.HasKey(RequiresKeyId))
        {
            IsLocked = false;
            inventory.RemoveKey(RequiresKeyId);
            Destroy(gameObject);
        }
    }

    public void Collide(Collision collision) { }
}