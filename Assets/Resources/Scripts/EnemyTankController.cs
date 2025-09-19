using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyTankController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float playerDetectRange = 15f;
    public Transform pointA;
    public Transform pointB;
    public float stopDistance = 2f;

    private Rigidbody rb;
    private Transform player;
    private Transform currentTarget;
    private int moveDirection = 1;

    public GameObject enemyBulletPrefab;
    public Transform firePoint;
    public float fireRate;
    private float nextFireTime = 0f;

    private enum EnemyState { Patrol, Chase }
    private EnemyState currentState = EnemyState.Patrol;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        currentTarget = pointA;
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            currentState = distance <= playerDetectRange ? EnemyState.Chase : EnemyState.Patrol;
        }

        switch (currentState)
        {
            case EnemyState.Patrol:
                Patrol();
                break;
            case EnemyState.Chase:
                Chase();
                break;
        }
    }

    void Patrol()
    {
        if (currentTarget == null) return;

        Vector3 move = transform.forward * moveSpeed * moveDirection * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);


        if (Vector3.Distance(transform.position, currentTarget.position) <= stopDistance)
        {
            if (currentTarget == pointA)
            {
                currentTarget = pointB;
                moveDirection = -1;
            }
            else
            {
                currentTarget = pointA;
                moveDirection = 1;
            }
        }
    }

    void Chase()
    {
        rb.linearVelocity = Vector3.zero;

        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(enemyBulletPrefab, firePoint.position, firePoint.rotation);

        Bullet b = bullet.GetComponent<Bullet>();
        if (b != null)
        {
            b.shooter = gameObject;
        }
    }
}
