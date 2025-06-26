using UnityEngine;

public class SkullAttackArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Monster monster = other.GetComponent<Monster>();

        if (monster != null)
        {
            monster.TakeDamage(Constants.maxDamage);
        }
    }
}
