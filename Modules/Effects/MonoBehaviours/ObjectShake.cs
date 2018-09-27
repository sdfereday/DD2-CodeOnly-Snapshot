using UnityEngine;

/// http://www.mikedoesweb.com/2012/camera-shake-in-unity/
public class ObjectShake : MonoBehaviour
{
    private Vector3 originPosition;
    // private Quaternion originRotation;

    public Transform TargetToShake;
    public float shake_decay = 0.002f;
    public float shake_intensity = .3f;

    private float temp_shake_intensity = 0;

    private void Start()
    {
        TargetToShake = TargetToShake == null ? transform : TargetToShake;
    }

    private void Update()
    {
        if (temp_shake_intensity > 0)
        {
            TargetToShake.position = originPosition + Random.insideUnitSphere * temp_shake_intensity;
            /*transform.rotation = new Quaternion(
                originRotation.x + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f,
                originRotation.y + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f,
                originRotation.z + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f,
                originRotation.w + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f);*/
            temp_shake_intensity -= shake_decay;
        }
    }

    public void Stop()
    {
        temp_shake_intensity = 0;
    }

    public void Shake()
    {
        originPosition = TargetToShake.position;
        // originRotation = TargetToShake.rotation;
        temp_shake_intensity = shake_intensity;
    }
}