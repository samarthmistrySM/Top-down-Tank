using UnityEngine;

public class TankTurretController : MonoBehaviour
{
    public Camera mainCamera;
    public float rotationSpeed = 1f;

    void Update()
    {
        RotateTowardsMouse();
    }

    void RotateTowardsMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        Plane helperPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;
        if (helperPlane.Raycast(ray, out rayDistance))
        {
            Debug.Log(rayDistance);
            Vector3 point = ray.GetPoint(rayDistance);
            Vector3 direction = point - transform.position;
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
    }
}
