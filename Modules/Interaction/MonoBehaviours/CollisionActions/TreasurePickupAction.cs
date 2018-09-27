using UnityEngine;

public class TreasurePickupAction : MonoBehaviour, IAction
{
    public int PickupValue = 1;

    public void Trigger(Collider other)
    {
        var wallet = other.GetComponent<Wallet>();

        if (wallet != null)
        {
            wallet.UpdateWallet(PickupValue);
            Destroy(gameObject);
        }
    }

    public void Collide(Collision collision) { }
}