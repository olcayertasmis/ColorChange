using UnityEngine;

public class RingController : MonoBehaviour
{
    [Header("Degiskenler")]
    [SerializeField] private float donmeHizi = 100f;

    private bool _isToLeft;

    private void FixedUpdate()
    {
        if (_isToLeft)
            transform.Rotate(0, 0, donmeHizi * Time.deltaTime);
        else
            transform.Rotate(0, 0, -donmeHizi * Time.deltaTime);
    }
} //Class