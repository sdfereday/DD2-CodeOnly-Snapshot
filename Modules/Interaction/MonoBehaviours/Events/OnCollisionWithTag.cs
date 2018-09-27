using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OnCollisionWithTag : MonoBehaviour
{
    public string TagRequired;
    private List<IAction> Actions;

    private void Start()
    {
        Actions = GetComponents<IAction>()
            .ToList();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagRequired))
        {
            Actions.ForEach(x => x.Trigger(other));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(TagRequired))
        {
            Actions.ForEach(x => x.Collide(collision));
        }
    }
}
