using UnityEngine;

public class SkullLegSmallArea : MonoBehaviour
{
    private void Update()
    {
        transform.position = InGameManager.Instance.GetPlayerObject().transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        Monster monster = other.GetComponent<Monster>();

        if (monster != null)
        {
            monster.TakeDamage(Constants.maxDamage);
        }
    }
}
