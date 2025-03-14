using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform door;  // Transform objek pintu
    public float openAngle = 90f;  // Sudut pintu terbuka
    public float openSpeed = 10f;  // Kecepatan rotasi pintu
    private bool isPlayerNearby = false;  // Status apakah pemain dekat pintu
    private bool isDoorOpen = false;  // Status pintu terbuka atau tertutup

    void Update()
    {
        // Cek apakah pemain dekat dan menekan tombol 'E'
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            // Toggle status pintu (terbuka atau tertutup)
            isDoorOpen = !isDoorOpen;
        }

        // Tentukan sudut rotasi pintu yang diinginkan
        float targetAngle = isDoorOpen ? openAngle : 0f;

        // Rotasikan pintu menuju sudut yang diinginkan
        door.rotation = Quaternion.RotateTowards(door.rotation, Quaternion.Euler(0f, targetAngle, 0f), openSpeed * Time.deltaTime);
    }

    // Trigger ketika pemain masuk ke area pintu
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("Player dekat dengan pintu.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            Debug.Log("Player menjauh dari pintu.");
        }
    
}
}
