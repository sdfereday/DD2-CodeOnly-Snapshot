using UnityEngine;

public class KeyPickupAction : MonoBehaviour, IAction
{
    public int keyId;

    public void Trigger(Collider other)
    {
        var inventory = other.GetComponent<PlayerInventory>();

        if (inventory != null)
        {
            if (inventory.HasKey(keyId))
            {
                throw new UnityException(ErrorConsts.DuplicateKeyError);
            }

            inventory.AddKey(keyId);
            Destroy(gameObject);
        }
    }

    public void Collide(Collision collision) { }
}