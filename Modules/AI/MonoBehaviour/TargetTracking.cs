using UnityEngine;

public class TargetTracking : MonoBehaviour
{
    // We can assume for now that the target of interest will always be the player
    private GameObject PlayerObject;

    // The root to measure numerics from such as distance (probably the root parent)
    public GameObject Root;

    // The FSM to set the data for (not overly generic but it's not a big deal)
    public PlayMakerFSM TrackingFSM;

    // Weights for this entity
    public float TargetAlertRangeValue = 5;
    public float TargetAttackRangeValue = 2;
    //public bool Sight = true;
    //public bool Smell = true;
    //public bool Sound = true;

    // Names for PlayerMaker variables (ensure names are correct, or PM will produce a false positive)
    // Add to consts please.
    public const string TargetVariable = "Target";
    public const string TargetRangeVariable = "TargetInRange";
    public const string TargetAttackRangeVariable = "TargetInAttackRange";
    public const string TargetVectorVariable = "TargetPositionVector";
    public const string EscapeVectorVariable = "EscapeVector";
    public const string DistToTargetVariable = "DistanceToTarget";

    // Sensors & Target Assessment
    private bool InLineOfSight()
    {
        return false;
    }

    private bool InDistanceOf(Vector3 Target, Vector3 Origin, float Value)
    {
        return GetDistanceToTarget(Target, Origin) <= Value;
    }

    private Vector3 PickEscapeVector(float maxDistance = 3f)
    {
        // get a random direction (360°) in radians
        float angle = Random.Range(0.0f, Mathf.PI * 2);

        // create a vector with length 1.0
        Vector3 V = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));

        // scale it to the desired length
        V *= 3f;

        Vector3 toPlayer = (PlayerObject.transform.position + V) - transform.position;
        //toPlayer += V;
        return V.normalized * -maxDistance;
    }

    private float GetDistanceToTarget(Vector3 Target, Vector3 Origin)
    {
        return Vector3.Distance(Target, Origin);
    }

    // Standard Unity Methods
    private void Start()
    {
        PlayerObject = GameObject.FindGameObjectWithTag(TagConsts.Player);
        TrackingFSM.FsmVariables.GetFsmGameObject(TargetVariable).Value = PlayerObject;
    }

    private void Update()
    {
        // Check for distances
        // ...

        // Check for LOS
        // ...

        // Check for whatever else
        // ...

        /// Set all the variables on the fsm
        // Set Meta Information
        TrackingFSM.FsmVariables.GetFsmFloat(DistToTargetVariable)
            .Value = GetDistanceToTarget(Root.transform.position, PlayerObject.transform.position);

        TrackingFSM.FsmVariables.GetFsmVector3(TargetVectorVariable)
            .Value = PlayerObject.transform.position;

        TrackingFSM.FsmVariables.GetFsmVector3(EscapeVectorVariable)
            .Value = PickEscapeVector();

        // In Range
        TrackingFSM.FsmVariables.GetFsmBool(TargetRangeVariable)
            .Value = InDistanceOf(Root.transform.position, PlayerObject.transform.position, TargetAlertRangeValue);

        // In Attack Range
        TrackingFSM.FsmVariables.GetFsmBool(TargetAttackRangeVariable)
            .Value = InDistanceOf(Root.transform.position, PlayerObject.transform.position, TargetAttackRangeValue);

    }
}
