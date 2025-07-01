using System.Collections;
using UnityEngine;

public class BlazingNovaPattern : IBossPattern
{ 
    private int ringCount;
    private float ringInterval;
    private int baseExplosionsPerRing;
    private int explosionStep;
    private string explosionEffectName;
    private GameObject fireworkEffectObject;
    private float explosionDelay;
    private float damage;
    private WaitForSeconds delay;

    public BlazingNovaPattern(int ringCount, float ringInterval, int baseExplosionsPerRing, int explosionStep, string explosionEffectName, GameObject fireworkEffectObject, float explosionDelay, float damage)
    {
        this.ringCount = ringCount;
        this.ringInterval = ringInterval;
        this.baseExplosionsPerRing = baseExplosionsPerRing;
        this.explosionStep = explosionStep;
        this.explosionEffectName = explosionEffectName;
        this.fireworkEffectObject = fireworkEffectObject;
        this.explosionDelay = explosionDelay;
        this.damage = damage;

        this.delay = new WaitForSeconds(explosionDelay);
    }

    public IEnumerator Execute(BossMonster boss)
    {
        fireworkEffectObject.SetActive(true);
        yield return new WaitForSeconds(1f);

        for (int ring = 1; ring <= ringCount; ring++)
        {
            float radius = ring * ringInterval;
            int explosionCount = baseExplosionsPerRing + (ring - 1) * explosionStep;

            for (int i = 0; i < explosionCount; i++)
            {
                float angle = (360f / explosionCount) * i;
                float rad = angle * Mathf.Deg2Rad;

                Vector3 offset = new Vector3(Mathf.Cos(rad), 0f, Mathf.Sin(rad)) * radius;
                Vector3 spawnPos = boss.transform.position + offset;

                ObjectPool.instance.SpawnFromPool(explosionEffectName, spawnPos, Quaternion.identity);
            }

            yield return delay;
        }

        fireworkEffectObject.SetActive(false);
        yield return new WaitForSeconds(1f);
    }
}
