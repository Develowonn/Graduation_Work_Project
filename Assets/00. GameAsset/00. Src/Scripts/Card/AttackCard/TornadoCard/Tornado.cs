using UnityEngine;

public class Tornado : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        Monster monster = other.GetComponent<Monster>();

        if(monster != null)
        {
            monster.TakeDamage(0);
        }
    }
}
