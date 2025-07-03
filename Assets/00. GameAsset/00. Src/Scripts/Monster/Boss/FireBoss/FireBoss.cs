using UnityEngine;

public class FireBoss : BossMonster
{
    [Header("Component")]
    private Animator animator;

    [Header("Flame")]
    [SerializeField] private string flameName;                  // 오브젝트 이름 (풀링)

    [Header("FlameTrailPattern")]
    [SerializeField] private float flameSpawnDelay;             // 화염 생성 간 딜레이    
    [SerializeField] private int flameCount;                    // 생성할 화염(1열 라인) 개수
    [SerializeField] private float distanceBetweenFlames;       // 화염 간 거리
    [SerializeField] private float trailFlameLifetime;          // 화염 지대 지속시간
    [SerializeField] private float flameTrailPatternDamage;     // 화염 지대 데미지

    [Header("ExplosionDashPattern")]
    [SerializeField] private float dashSpeed;                   // 대쉬 속도
    [SerializeField] private float dashDuration;                // 대쉬 시간
    [SerializeField] private float dashFlameLifetime;           // 화염 지대 지속시간
    [SerializeField] private float explosionDashPatternDamage;  // 대쉬 패턴 데미지

    [Header("BlazingNovaPattern")]
    [SerializeField] private int ringCount;                                      // 링 개수
    [SerializeField] private float ringInterval;                                 // 링 간격
    [SerializeField] private int baseExplosionsPerRing;                          // 링당 폭발 개수
    [SerializeField] private int explosionStep;                                  // 링당 폭발 개수 증가량
    [SerializeField] private string explosionEffectName;                         // 폭발 이펙트 이름
    [SerializeField] private GameObject fireworkEffectObject;                    // 폭죽 오브젝트  
    [SerializeField] private float explosionDelay;                               // 폭발 딜레이
    [SerializeField] private float blazingNovaPatternDamage;                     // 노바 패턴 데미지

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    protected override void Start()
    {
        AddPattern(new FlameTrailPattern(animator, flameSpawnDelay, flameCount, flameName, distanceBetweenFlames, trailFlameLifetime, flameTrailPatternDamage));
        AddPattern(new ExplosionDashPattern(dashSpeed, dashDuration, flameName, dashFlameLifetime, explosionDashPatternDamage));
        AddPattern(new BlazingNovaPattern(animator, ringCount, ringInterval, baseExplosionsPerRing, explosionStep, explosionEffectName, fireworkEffectObject, explosionDelay, blazingNovaPatternDamage));
        base.Start();
    }
}
