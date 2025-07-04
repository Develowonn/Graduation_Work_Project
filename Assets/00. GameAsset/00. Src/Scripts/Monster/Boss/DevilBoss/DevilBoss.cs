using UnityEngine;

public class DevilBoss : BossMonster
{
    [Header("Component")]
    private Animator animator;

    [Header("SkullExplosion")]
    [SerializeField] private string chargingEffect;                     // 차지 이펙트
    [SerializeField] private string skullEffect;                        // 해골 이펙트
    [SerializeField] private Transform skullExplosionSkillPos;          // skullExposion 스킬 발동 위치

    [Header("BeamRain")]
    [SerializeField] private float beamRainRange;                       // 범위
    [SerializeField] private string beamEffectName;                     // 레인 이펙트 이름
    [SerializeField] private int beamCount;                             // 횟수
    [SerializeField] private float beamRainDelayTime;                   // 빔 발동 후 약간의 딜레이 시간

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    protected override void Start()
    {
        AddPattern(new SkullExplosion(chargingEffect, skullEffect, skullExplosionSkillPos, animator));
        AddPattern(new BeamRainPattern(beamRainRange, beamEffectName, beamCount, beamRainDelayTime, animator));
        base.Start();
    }
}
