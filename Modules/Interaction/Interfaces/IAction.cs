using UnityEngine;

public interface IAction
{
    void Trigger(Collider other);
    void Collide(Collision collision);
}