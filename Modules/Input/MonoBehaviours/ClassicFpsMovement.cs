using UnityEngine;

public class ClassicFpsMovement : MonoBehaviour
{
    public float rotateSpeed = 5f;
    public float movementSpeed = 2f;
    public Rigidbody rbody;

    private bool isEnabled = true;
    private float inputX = 0f;
    private float inputZ = 0f;

    private void FixedUpdate()
    {
        // TODO: Fix weird speed up on frame skipping.
        if (!isEnabled)
        {
            return;
        }

        rbody.velocity = Vector3.zero;
        inputX = Input.GetAxisRaw("Horizontal");
        inputZ = Input.GetAxisRaw("Vertical");

        if (inputX != 0)
            rotate();
        if (inputZ != 0)
            move();
    }

    private void rotate()
    {
        transform.Rotate(new Vector3(0f, (inputX * rotateSpeed) * Time.fixedDeltaTime, 0f));
    }

    private void move()
    {
        //transform.position += transform.forward * inputZ * movementSpeed * Time.deltaTime;
        rbody.velocity = (transform.forward * inputZ * movementSpeed) * Time.fixedDeltaTime; // * Time.deltaTime;
    }

    private void OnEnable()
    {
        Mapper.OnMapGenerated += PlaceAtStart;
    }

    private void OnDisable()
    {
        Mapper.OnMapGenerated -= PlaceAtStart;
    }

    private void PlaceAtStart(Mapper.MapResult result)
    {
        if (result.StartPoint == null)
            return;

        var XValue = result.StartPoint.x;
        var ZValue = result.StartPoint.y;
        transform.position = new Vector3(XValue, transform.position.y, ZValue);
    }

    public void ToggleMovement(bool enable)
    {
        isEnabled = enable;
    }
}