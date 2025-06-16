using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private FacingDirection   facingDirection;

    private int               isMovingHash;
    private int               xDirHash;

    private Animator          animator;
    private MovementRigidbody movementRigidbody;

    private void Start()
    {
        facingDirection   = FacingDirection.Forward;

        isMovingHash      = Animator.StringToHash("IsMove");
        xDirHash          = Animator.StringToHash("XDir");

        animator          = GetComponent<Animator>();
        movementRigidbody = GetComponent<MovementRigidbody>();
    }

    private void Update()
    {
        MoveToForward();
    }

    private void MoveToForward()
    {
        movementRigidbody.MoveToLocal(Vector3.forward);
    }
}
