using UnityEngine;

public class SkullLegBigArea : MonoBehaviour
{
    private void Update()
    {
        transform.rotation = Quaternion.Euler(-90, 0 , 0);
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
