using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float speedBoostAmount = 2f;
    public GameObject collisionIndicator;
    public GameObject specialCollisionIndicator;
    public Transform cameraTransform; 

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
          
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);

            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
    }
}
