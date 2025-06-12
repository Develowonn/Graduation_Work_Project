using UnityEngine;

public class MovementRigidbody : MonoBehaviour
{
    public Vector3      MovementDir { get; private set; }

    [SerializeField]
    private float       movementSpeed;
    private Rigidbody   rigid;

    private void Start()
    {
        rigid    = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rigid.linearVelocity = MovementDir.normalized * movementSpeed;
    }

    public void MoveTo(Vector3 moveDir)
    {
        MovementDir = moveDir;
    }
}