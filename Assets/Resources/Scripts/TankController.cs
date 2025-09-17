using UnityEngine;

public class TankController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 100f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 moveDirection = moveSpeed * Time.fixedDeltaTime * vertical * transform.forward;
        rb.MovePosition(rb.position + moveDirection);

        Quaternion turn = Quaternion.Euler(0f, horizontal * turnSpeed * Time.fixedDeltaTime, 0f);
        rb.MoveRotation(rb.rotation * turn);
    }
}
