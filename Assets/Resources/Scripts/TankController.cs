using UnityEngine;
using UnityEngine.PlayerLoop;

public class TankController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 100f;
    private Rigidbody rb;
    public GameObject playerBulletPrefab;
    public Transform firePoint;
    public float fireRate = 100f;
    private float nextFireTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
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

    void Shoot()
    {
        Instantiate(playerBulletPrefab, firePoint.position, firePoint.rotation);
    }
}
