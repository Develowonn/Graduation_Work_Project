
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomEnemysTargetingStrategy : IMultiTargetingStrategy
{
    private float radius;
    private int count;
    private LayerMask targetMask;

    public RandomEnemysTargetingStrategy(float radius, int count, LayerMask targetMask)
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
            .Select(h => h.transform)           // Trasnform ����
            .OrderBy(_ => Random.value)         // ��������
            .Take(count)                        // ���ĵȰ��� �������
            .ToList();                          // List���·� ��ȯ
    }
}
