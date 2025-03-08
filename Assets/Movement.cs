using UnityEngine;

public class Movement : MonoBehaviour
{
public float moveSpeed = 5f;
    public float speedBoostAmount = 2f;
    public GameObject collisionIndicator;
    public GameObject specialCollisionIndicator; 
    public GameObject followSpherePrefab;

    private Vector3 moveDirection;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (collisionIndicator != null)
        {
            collisionIndicator.SetActive(false);
        }
        if (specialCollisionIndicator != null)
        {
            specialCollisionIndicator.SetActive(false);
        }
    }

    void Update()
    {

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        moveDirection = new Vector3(moveX, 0, moveZ).normalized;


        if (moveDirection.magnitude >= 0.1f)
        {
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("spdup"))
        {
            Debug.Log("Speed boost triggered! Speed increased by: " + speedBoostAmount);
            moveSpeed += speedBoostAmount;
            Debug.Log("New speed: " + moveSpeed);
            
            if (collisionIndicator != null)
            {
                collisionIndicator.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Collided with: " + other.gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Sembunyikan tanda saat keluar dari kontak
        if (other.gameObject.CompareTag("spdup") && collisionIndicator != null)
        {
            collisionIndicator.SetActive(false);
        }
    }
}
