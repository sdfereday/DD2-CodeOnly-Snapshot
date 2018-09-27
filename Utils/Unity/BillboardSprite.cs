using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class BillboardSprite : MonoBehaviour
{
    public enum PAIR_DIRECTION
    {
        FRONT,
        RIGHT,
        REAR,
        LEFT
    }

    [System.Serializable]
    public class SpritePair
    {
        public PAIR_DIRECTION AtDirection;
        public Sprite ShowSprite;
    }

    private Transform Target;
    private SpriteRenderer spr;
    private PAIR_DIRECTION lastDirection;
    private PAIR_DIRECTION currentDirection;
    
    public Transform Container;
    public List<SpritePair> SpriteList;

    private float CalculateAngle(Vector3 from, Vector3 to)
    {
        return Quaternion.FromToRotation(Vector3.up, to - from).eulerAngles.z;
    }

    private void Start()
    {
        Target = GameObject.FindGameObjectWithTag(TagConsts.CameraTag).transform;

        spr = GetComponent<SpriteRenderer>();
    }

    float angle360(Vector3 from, Vector3 to, Vector3 right)
    {
        float angle = Vector3.Angle(from, to);
        return (Vector3.Angle(right, to) > 90f) ? 360f - angle : angle;
    }

    private void Update()
    {
        Vector3 dir = Target.forward;
        dir.y = 0.0f;

        if (dir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(dir);

        // In case it is needed.
        Vector3 targetVector = Target.position - Container.position;

        // A) The forward facing position of this entity
        Vector3 transformDirection = Container.rotation * Vector3.forward;
        Debug.DrawRay(Container.position, transformDirection, Color.blue, 0f, true);

        // B) The forward facing position of the target
        Vector3 targetDirection = Target.rotation * Vector3.forward;
        Debug.DrawRay(Target.position, targetDirection, Color.red, 0f, true);

        // This will get you a degree-based angle between the parent and the target.
        float angleBetween = Vector3.Angle(transformDirection, targetDirection);

        Debug.Log(angle360(transformDirection, targetDirection, Vector3.right));
        
        // Use this to determine which side it is to the player: transform.parent.position.x > mainCamera.transform.position.x
        // Presumably we shouldn't be able to see further than a 180 angle, if we do, things might get weird.
        // A cheap and dirty hack to find out if you're on the left or right would simpy be a case of checking the x value.
        //currentDirection = Agent.velocity.normalized.z < 0 ? PAIR_DIRECTION.FRONT : PAIR_DIRECTION.REAR;

        if (angleBetween < 140 && angleBetween >= 40)
        {
            //currentDirection = Agent.velocity.normalized.x < 0 ? PAIR_DIRECTION.RIGHT : PAIR_DIRECTION.LEFT;
        }
        
        if (lastDirection != currentDirection)
        {
            spr.sprite = SpriteList.Find(x => x.AtDirection == currentDirection).ShowSprite;
        }

        lastDirection = currentDirection;

    }
}
