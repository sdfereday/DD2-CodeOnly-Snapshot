using UnityEngine;
using System.Collections.Generic;

public class AgentBillboardSprite : MonoBehaviour
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

    public List<SpritePair> SpriteList;
    public Animator anim;

    private Transform Player;
    private SpriteRenderer spr;
    private PAIR_DIRECTION lastDirection;
    private PAIR_DIRECTION currentDirection;

    private Vector3 curPos;
    private Vector3 lastPos;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag(TagConsts.Player).transform;
        spr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector3 dir = Player.forward;
        dir.y = 0.0f;

        if (dir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(dir);

        //Debug.DrawRay(transform.position, transformDirection, Color.blue, 0f, true);
        //Debug.DrawRay(Target.position, targetDirection, Color.red, 0f, true); //Target.rotation * Vector3.forward;
        //Debug.DrawRay(Target.rotation * Vector3.forward, targetDirection, Color.yellow, 0f, true);

        // This will get you a degree-based angle between the parent and the target.
        // A) The forward facing position of this entity
        Vector3 transformDirection = transform.parent.rotation * Vector3.forward;

        // B) The forward facing position of the target
        Vector3 targetDirection = Vector3.forward;

        // C) The travelling direction of the transform in relation to the target
        Vector3 direction = (transformDirection - targetDirection).normalized;

        bool onRight = transform.parent.InverseTransformPoint(Player.position).x > 0.0f;
        float angleBetween = Vector3.Angle(transformDirection, Player.rotation * Vector3.forward);
        float y = Mathf.Round(angleBetween / 90) * 90;

        curPos = transform.parent.position;
        anim.SetBool("IsMoving", true);

        if (curPos == lastPos)
        {
            anim.SetInteger("Mode", 0);
            anim.SetBool("IsMoving", false);
            anim.Play("Idle");
            lastPos = curPos;
            return;
        }

        /// Simple enough, just check the direct angle between front or rear.
        // Entity is facing the target
        if (y == 0 || y == 360)
        {
            currentDirection = PAIR_DIRECTION.REAR;
            anim.SetInteger("Mode", 1);
        }

        // Entity is facing away from the target
        if (y == 180)
        {
            currentDirection = PAIR_DIRECTION.FRONT;
            anim.SetInteger("Mode", 2);
        }

        /// A little more complex, in either event, we have to check what side of the target we are on so we can mirror the effect.
        // Entity is facing the right of the target
        if (y == 270)
        {
            currentDirection = direction.x * direction.z > 0 ? (onRight ? PAIR_DIRECTION.RIGHT : PAIR_DIRECTION.LEFT) : (onRight ? PAIR_DIRECTION.RIGHT : PAIR_DIRECTION.LEFT);
            anim.SetInteger("Mode", currentDirection == PAIR_DIRECTION.RIGHT ? 3 : 4);
        }

        // Entity is facing the left of the target
        if (y == 90)
        {
            currentDirection = direction.z * direction.x < 0 ? (onRight ? PAIR_DIRECTION.RIGHT : PAIR_DIRECTION.LEFT) : (onRight ? PAIR_DIRECTION.RIGHT : PAIR_DIRECTION.LEFT);
            anim.SetInteger("Mode", currentDirection == PAIR_DIRECTION.RIGHT ? 3 : 4);
        }

        /// Apply the direction only if it's changed (saves on performance a bit).
        if (lastDirection != currentDirection)
        {
            //spr.sprite = SpriteList.Find(x => x.AtDirection == currentDirection).ShowSprite;
        }

        lastDirection = currentDirection;
        lastPos = curPos;
    }
}