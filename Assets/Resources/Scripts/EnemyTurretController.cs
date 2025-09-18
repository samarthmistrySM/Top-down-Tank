using UnityEngine;

public class EnemyTurretController : MonoBehaviour
{
    public Transform player;
    private Transform tankBody;
    public float rotationSpeed = 2f;

    public float playerDetectRange = 15f;

    void Start()
    {
        tankBody = transform.parent;
    }

    void Update()
    {
        if (tankBody == null || player == null) return;

        float distance = Vector3.Distance(tankBody.position, player.position);
        if (distance >= playerDetectRange) return;

        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }

    void OnDestroy()
    {
        Destroy(this);
    }
}
