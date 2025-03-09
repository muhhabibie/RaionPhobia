using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public Transform lid;
    public GameObject uiText; // UI "Press E to Open"

    private bool isOpened = false;
    private bool isPlayerNearby = false;
    private float openSpeed = 100f;
    private float targetRotation = -35f;
    private bool isOpening = false;

    private Renderer chestRenderer;
    private Color defaultEmissionColor;
    private Color highlightEmissionColor = Color.yellow;

    void Start()
    {
        chestRenderer = GetComponent<Renderer>();

        if (chestRenderer.material.HasProperty("_EmissionColor"))
        {
            defaultEmissionColor = chestRenderer.material.GetColor("_EmissionColor");
        }

        if (uiText != null)
        {
            uiText.SetActive(false); // Sembunyikan teks di awal
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E) && !isOpened)
        {
            isOpened = true;
            isOpening = true;
            Debug.Log("Peti mulai terbuka!");

            if (uiText != null)
            {
                uiText.SetActive(false); // Sembunyikan UI saat peti dibuka
            }
        }

        if (isOpening && lid != null)
        {
            Quaternion targetRotationQuat = Quaternion.Euler(targetRotation, lid.rotation.eulerAngles.y, lid.rotation.eulerAngles.z);
            lid.rotation = Quaternion.RotateTowards(lid.rotation, targetRotationQuat, openSpeed * Time.deltaTime);

            if (Quaternion.Angle(lid.rotation, targetRotationQuat) < 1f)
            {
                isOpening = false;
                Debug.Log("Peti sudah terbuka sepenuhnya!");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("Player mendekati peti. Tekan 'E' untuk membuka.");

            if (uiText != null)
            {
                uiText.SetActive(true); 
            }

            if (chestRenderer.material.HasProperty("_EmissionColor"))
            {
                chestRenderer.material.SetColor("_EmissionColor", highlightEmissionColor * 2f);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            Debug.Log("Player menjauh dari peti.");

            if (uiText != null)
            {
                uiText.SetActive(false);
            }

            if (chestRenderer.material.HasProperty("_EmissionColor"))
            {
                chestRenderer.material.SetColor("_EmissionColor", defaultEmissionColor);
            }
        }
    }
}
