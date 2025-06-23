using UnityEngine;

public class MovementRigidbody : MonoBehaviour
{
    public Vector3          MovementDir { get; private set; }

    [SerializeField]
    private CharacterType   characterType;
    [SerializeField]
    private float           movementSpeed;

    private PlayerStat      playerStat;
    private Rigidbody       rigid;

    private void Start()
    {
        rigid    = GetComponent<Rigidbody>();

        if (characterType == CharacterType.Player)
            playerStat = GetComponent<PlayerStat>();
    }

    private void Update()
    {
        switch (characterType)
        {
            case CharacterType.Player:
                movementSpeed = playerStat.GetMovementSpeedStat();
                break;
        }
    }

    private void FixedUpdate()
    {
        rigid.linearVelocity = MovementDir.normalized * movementSpeed;
    }

    public void MoveTo(Vector3 moveDir)
    {
        MovementDir = moveDir;
    }

	public void MoveToLocal(Vector3 moveDir)
	{
		MovementDir = transform.TransformDirection(moveDir);
	}
}