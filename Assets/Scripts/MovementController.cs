using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Header("Movement stats")]
    [SerializeField] private float movementSpeed;

    // references
    private CharacterController characterController;
    private Camera mainCamera;

    // for animations
    private Vector3 lookAtPoint;
    private Vector3 inputVector;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        MoveCharacter();
        LookAtCursor();
    }

    private void MoveCharacter()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        inputVector = new Vector3(horizontalInput, 0, verticalInput);

        Vector3 direction = inputVector.normalized;
        
        characterController.Move(direction * movementSpeed * Time.deltaTime);
    }

    private void LookAtCursor()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if(groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);

            Debug.DrawLine(ray.origin, point, Color.red);

            lookAtPoint = new Vector3(point.x, transform.position.y, point.z);

            transform.LookAt(lookAtPoint);
        }
    }

    public Vector3 GetInputVector()
    {
        return inputVector;
    }

    public Vector3 GetLookAtPoint()
    {
        return lookAtPoint;
    }
}
