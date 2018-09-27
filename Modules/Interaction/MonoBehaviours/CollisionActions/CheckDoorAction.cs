using UnityEngine;

public class CheckDoorAction : MonoBehaviour, IAction
{
    public PlayMakerFSM FSM;

    public void Trigger(Collider other)
    {
        var door = other.GetComponent<Door>();

        if (door != null && door.IsLocked)
        {
            Debug.Log("Reset path to door.");
            FSM.SendEvent(FsmConsts.Events.ResetPath);
        }
    }

    public void Collide(Collision collision) { }
}
