using UnityEngine;

public class HitAction : MonoBehaviour, IAction
{
    public ActorStats Stats;
    public ALIGNMENT_TYPE TakesDamageFrom;

    public void Trigger(Collider other)
    {
        IDamage inboundDamage = other.GetComponent<IDamage>();

        if (inboundDamage != null && inboundDamage.AlignmentType == TakesDamageFrom)
        {
            Debug.Log(transform.parent.name + " should take some damage: " + inboundDamage.DamageValue);
            Stats.DescreaseHealth(inboundDamage.DamageValue);
        }
    }

    public void Collide(Collision collision) { }
}