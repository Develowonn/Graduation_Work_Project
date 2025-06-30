using UnityEngine;

public class FireBoss : BossMonster
{
    [Header("Flame")]
    [SerializeField] private string flameName;                  // 오브젝트 이름 (풀링)

    [Header("FlameTrailPattern")]
    [SerializeField] private float flameSpawnDelay;             // 화염 생성 간 딜레이    
    [SerializeField] private int flameCount;                    // 생성할 화염(1열 라인) 개수
    [SerializeField] private float distanceBetweenFlames;       // 화염 간 거리
    [SerializeField] private float trailFlameLifetime;          // 화염 지대 지속시간

    [Header("ExplosionDashPattern")]
    [SerializeField] private float dashSpeed;                   // 대쉬 속도
    [SerializeField] private float dashDuration;                // 대쉬 시간
    [SerializeField] private float dashFlameLifetime;           // 화염 지대 지속시간

    protected override void Start()
    {
        AddPattern(new FlameTrailPattern(flameSpawnDelay, flameCount, flameName, distanceBetweenFlames, trailFlameLifetime));
        AddPattern(new ExplosionDashPattern(dashSpeed, dashDuration, flameName, dashFlameLifetime));
        base.Start();
    }
}
