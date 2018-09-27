using UnityEngine;

public class HitFSMAction : MonoBehaviour, IAction
{
    public PlayMakerFSM FSM;
    public ALIGNMENT_TYPE TakesDamageFrom;

    public void Trigger(Collider other)
    {
        IDamage inboundDamage = other.GetComponent<IDamage>();

        if (inboundDamage != null && inboundDamage.AlignmentType == TakesDamageFrom)
        {
            Debug.Log(transform.parent.name + " should take some damage: " + inboundDamage.DamageValue);

            FSM.FsmVariables.GetFsmInt(FsmConsts.Variables.InboundDamage)
                .Value = inboundDamage.DamageValue;

            FSM.SendEvent(FsmConsts.Events.ApplyDamage);
        }
    }

    public void Collide(Collision collision) { }
}