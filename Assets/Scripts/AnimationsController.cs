using UnityEngine;

public class AnimationsController : MonoBehaviour
{

    private static readonly int animVertical = Animator.StringToHash("Vertical");
    private static readonly int animHorizontal = Animator.StringToHash("Horizontal");
    private static readonly int animIsDead = Animator.StringToHash("IsDead");

    private MovementController movementController;
    private Animator animator;

    private bool isDead;

    private void Start()
    {
        animator = GetComponent<Animator>();
        movementController = GetComponent<MovementController>();

        isDead = false;
        animator.ResetTrigger(animIsDead);
    }

    private void Update()
    {
        if (isDead)
            return;

        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        float forwardBackwardsMagnitude = 0;
        float rightLeftMagnitude = 0;

        Vector3 inputVector = movementController.GetInputVector();
        Vector3 lookAtPoint = movementController.GetLookAtPoint();

        if (inputVector.magnitude > 0)
        {
            Vector3 normalizedLookingAt = lookAtPoint - transform.position;
            normalizedLookingAt.Normalize();
            forwardBackwardsMagnitude = Mathf.Clamp(Vector3.Dot(inputVector, normalizedLookingAt), -1, 1);

            Vector3 perpendicularLookingAt = new Vector3(normalizedLookingAt.z, 0, -normalizedLookingAt.x);
            rightLeftMagnitude = Mathf.Clamp(Vector3.Dot(inputVector, perpendicularLookingAt), -1, 1);
        }

        // update the animator parameters
        animator.SetFloat(animVertical, forwardBackwardsMagnitude);
        animator.SetFloat(animHorizontal, rightLeftMagnitude);
    }

    public void OnDeath()
    {
        isDead = true;

        animator.SetTrigger(animIsDead);
    }
}
