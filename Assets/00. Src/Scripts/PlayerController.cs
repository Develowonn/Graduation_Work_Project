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
        InputMovementKey();
        UpdateRotation();
        UpdateAnimation();
    }

    private void InputMovementKey()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        movementRigidbody.MoveTo(new Vector3(x, 0, z));
    }

    private void UpdateRotation()
    {
        if(movementRigidbody.MovementDir.z <= Constants.MovementDirection.Backward)
        {
            facingDirection    = FacingDirection.Backward;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if(movementRigidbody.MovementDir.z >= Constants.MovementDirection.Forward)
        {
            facingDirection    = FacingDirection.Forward;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void UpdateAnimation()
    {
        bool isMoving = movementRigidbody.MovementDir != Vector3.zero;

        if(animator.GetBool(isMovingHash) != isMoving)
            animator.SetBool(isMovingHash, isMoving);

        if (!isMoving) return;

        animator.SetFloat(xDirHash, GetMovementBlendValue());
    }

    private float GetMovementBlendValue()
    {
        if (movementRigidbody.MovementDir.x <= Constants.MovementDirection.Left)
            return facingDirection == FacingDirection.Forward
                ? Constants.BlendTreeThreshold.PlayerMove.RunLeft
                : Constants.BlendTreeThreshold.PlayerMove.RunRight;

        else if (movementRigidbody.MovementDir.x >= Constants.MovementDirection.Right)
            return facingDirection == FacingDirection.Forward
                ? Constants.BlendTreeThreshold.PlayerMove.RunRight
                : Constants.BlendTreeThreshold.PlayerMove.RunLeft;

        return Constants.BlendTreeThreshold.PlayerMove.Run;
    }
}
