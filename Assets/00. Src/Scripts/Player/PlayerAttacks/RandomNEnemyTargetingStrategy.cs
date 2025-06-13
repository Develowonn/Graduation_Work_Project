using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomNEnemyTargetingStrategy : IMultiTargetingStrategy
{
    private float radius;
    private int count;
    private LayerMask targetMask;

    public RandomNEnemyTargetingStrategy(float radius, int count, LayerMask targetMask)
    {
        this.radius = radius;
        this.count = count;
        this.targetMask = targetMask;
    }

    public void Init(float radius, int count, LayerMask targetMask)
    {
        this.radius = radius;
        this.count = count;
        this.targetMask = targetMask;
    }

    public List<Transform> GetTargets(Transform origin)
    {
        Collider[] hits = Physics.OverlapSphere(origin.position, radius, targetMask);

        return hits
            .Select(h => h.transform)           // Trasnform 추출
            .OrderBy(_ => Random.value)         // 랜덤정렬
            .Take(count)                        // 정렬된것중 상위몇개만
            .ToList();                          // List형태로 변환
    }
}
