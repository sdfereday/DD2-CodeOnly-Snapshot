using UnityEngine;

public class ShakeCamera : MonoBehaviour, IAction
{
    public CameraControl ObjectShakeComponent;

    public void Trigger(Collider other)
    {
        ObjectShakeComponent.Shake();
    }

    public void Collide(Collision collision) {}
}