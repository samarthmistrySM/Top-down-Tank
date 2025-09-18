using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 3f;
    public GameObject shooter;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.linearVelocity = transform.forward * speed;
        Destroy(gameObject, lifeTime);
        transform.Rotate(-90f, 0f, 0f, Space.Self);
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject == shooter) return;

        Health targetHealth = other.gameObject.GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(10)
    ;
        }

        Destroy(gameObject);
    }
}
