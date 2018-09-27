using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OnCollidingWithObject : MonoBehaviour
{
    private List<IAction> Actions;
    private void Start()
    {
        Actions = GetComponents<IAction>()
            .ToList();
    }

    private void OnTriggerStay(Collider other)
    {
        Actions.ForEach(x => x.Trigger(other));
    }

    private void OnCollisionEnter(Collision collision)
    {
        Actions.ForEach(x => x.Collide(collision));
    }
}
