using UnityEngine;

public class MeleeWeapon : MonoBehaviour, IDamage
{
    public ALIGNMENT_TYPE Alignment;
    public ALIGNMENT_TYPE AlignmentType
    {
        get
        {
            return Alignment;
        }
    }

    public int DamageOutputValue = 1;
    public int DamageValue
    {
        get
        {
            return DamageOutputValue;
        }
    }
}