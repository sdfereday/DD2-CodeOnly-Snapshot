using UnityEngine;
using System.Collections;

public class LerpRandomRange : MonoBehaviour
{

    private Light lighting;

    private float startrange = 10f;
    private float targetRange = 0.0f;
    private bool flipped = false;

	public float minRange = 12.0f;
    public float maxRange = 20.0f;
    public float speed = 10f;


    void Start()
    {
        lighting = gameObject.GetComponent<Light>();
		lighting.range = minRange;
        startrange = lighting.range;
    }

    void Update()
    {

        // This will make it move from one range, then back again like a straight curve. When it hits the other end of the scale,
        // you'll need to pick a new random value above the start range and between a max range, then it'll go again.
        // There's probably an uber simpler way to do this. Have a look at the facing method in player input, that might work.
        // https://unity3d.com/learn/tutorials/modules/beginner/scripting/linear-interpolation
        if (lighting.range <= targetRange - 0.5f && !flipped) {
            lighting.range = Mathf.Lerp(lighting.range, targetRange, speed * Time.deltaTime);
        } else
        {
            flipped = true;
            lighting.range = Mathf.Lerp(lighting.range, startrange, speed * Time.deltaTime);
            if (lighting.range <= startrange + 0.5f)
            {
                targetRange = Random.Range(minRange, maxRange);
                flipped = false;
            }
        }

    }

}