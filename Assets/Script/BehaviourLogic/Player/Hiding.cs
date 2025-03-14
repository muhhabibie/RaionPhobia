using UnityEngine;

public class HideInGrass : MonoBehaviour
{
    private bool isHidden = false;  
    private Renderer playerRenderer;
    private Collider currentGrass;

    void Start()
    {
        playerRenderer = GetComponent<Renderer>(); // Ambil Renderer dari Player
    }

    void Update()
    {
        // Tekan "E" untuk bersembunyi di rumput
        if (Input.GetKeyDown(KeyCode.E) && currentGrass != null)
        {
            ToggleHide();
        }
    }

    void ToggleHide()
    {
        isHidden = !isHidden;

        if (isHidden)
        {
            Debug.Log("Player bersembunyi!");
            playerRenderer.enabled = false; // Sembunyikan model player
        }
        else
        {
            Debug.Log("Player keluar dari rumput!");
            playerRenderer.enabled = true; // Tampilkan kembali player
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grass")) 
        {
            Debug.Log("Masuk ke rumput!");
            currentGrass = other;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Grass")) 
        {
            Debug.Log("Keluar dari rumput!");
            currentGrass = null;
            isHidden = false;
            playerRenderer.enabled = true; 
        }
    }
}
