using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private FacingDirection   facingDirection;

    private int               isMovingHash;
    private int               xDirHash;

    private Animator          animator;

    private PlayerStat        playerStat;
    private MovementRigidbody movementRigidbody;

    private void Start()
    {
        facingDirection   = FacingDirection.Forward;

        isMovingHash      = Animator.StringToHash("IsMove");
        xDirHash          = Animator.StringToHash("XDir");

        animator          = GetComponent<Animator>();

        playerStat        = GetComponent<PlayerStat>();
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

    public void TakeDamage(float damage)
    {
        float reduceedDamage = damage * (1.0f - playerStat.GetDamageReduction());
        playerStat.ReduceHP(reduceedDamage);

        if(playerStat.GetCurrentHP() <= 0.0f)
        {

        }
    }
}
