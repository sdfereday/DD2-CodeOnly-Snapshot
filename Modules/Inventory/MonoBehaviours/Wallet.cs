using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField]
    private int value;

    public void UpdateWallet(int _value)
    {
        value = value + _value < 0 ? 0 : _value;
    }
}
