using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Other Compenents")]
    [SerializeField] private Transform ball;

    private void LateUpdate()
    {
        if (ball.position.y > transform.position.y)
            transform.position = new Vector3(transform.position.x, ball.position.y, transform.position.z);
    }
} //Class