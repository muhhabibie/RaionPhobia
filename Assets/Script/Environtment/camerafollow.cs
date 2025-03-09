using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Pemain
    public Vector3 offset = new Vector3(-212, 336, -235); // Posisi kamera tetap di belakang atas pemain
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        // Pastikan kamera hanya mengikuti posisi karakter tanpa ikut berputar
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Kunci rotasi kamera agar tidak ikut karakter
        transform.rotation = Quaternion.Euler(45f, 45f, 0f); // Bisa disesuaikan sudutnya
    }
}
