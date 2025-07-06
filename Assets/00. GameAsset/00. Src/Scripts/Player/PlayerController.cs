using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private FacingDirection   facingDirection;

    private int               isMovingHash;

    private Animator          animator;

    private PlayerStat        playerStat;
    private MovementRigidbody movementRigidbody;

    [SerializeField] private float damageInterval = 1f; // 피해 간격
    private float lastDamageTime;

    private void Awake()
    {
        animator          = GetComponent<Animator>();

        playerStat        = GetComponent<PlayerStat>();
        movementRigidbody = GetComponent<MovementRigidbody>();
    }

    private void Start()
    {
        facingDirection   = FacingDirection.Forward;
        isMovingHash      = Animator.StringToHash("isMovable");
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
            StageManager.instance.EndGame(TimeManager.instance.GetCurrentTime(), GameEndType.Defeat);
        }
    }

	public void TakeDamagePercent(float damagePercent)
	{
		playerStat.ReduceHPByPercent(damagePercent);

		if (playerStat.GetCurrentHP() <= 0.0f)
		{
			StageManager.instance.EndGame(TimeManager.instance.GetCurrentTime());
		}
	}

	public void SetMovable(bool value)
    {
        switch (value) 
        {
            case true:
                movementRigidbody.SetMovable(true);
                animator.SetBool(isMovingHash, true);
                break;

            case false:
                movementRigidbody.SetMovable(false);
                animator.SetBool(isMovingHash, false);
                break;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Monster"))
        {
            if (Time.time >= lastDamageTime + damageInterval)
            {
                TakeDamage(collision.collider.GetComponent<Monster>().GetAttackPower());
                lastDamageTime = Time.time;
            }
        }
    }
}
