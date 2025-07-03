using UnityEngine;

public class DevilBoss : BossMonster
{
    [Header("Component")]
    private Animator animator;

    [Header("SkullExplosion")]
    [SerializeField] private string chargingEffect;
    [SerializeField] private string skullEffect;
    [SerializeField] private Transform skullExplosionSkillPos;


    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    protected override void Start()
    {
        AddPattern(new SkullExplosion(chargingEffect, skullEffect, skullExplosionSkillPos, animator));
        base.Start();
    }
}
