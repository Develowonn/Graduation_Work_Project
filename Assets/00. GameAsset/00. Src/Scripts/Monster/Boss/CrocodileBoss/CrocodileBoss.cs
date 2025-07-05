using System.Collections;
using UnityEngine;

public class CrocodileBoss : BossMonster
{
    [Header("Component")]
    private Animator animator;

    [Header("AxeSlashPattern")]
    [SerializeField] private string effectName;
    [SerializeField] private int slashCount;
    [SerializeField] private float slashDuration;
    [SerializeField] private float slashDamage;
    [SerializeField] private float slashAttackDelay;
    [SerializeField] private float slashSpeed;

    [Header("GroundSpike")]
    [SerializeField] private string mainEffectName;
    [SerializeField] private string subEffectName;
    [SerializeField] private float subSpikeDelayTime;
    [SerializeField] private float mainSpikeDamage;
    [SerializeField] private float subSpikeDamage;
    [SerializeField] private float subEffectRange;
    [SerializeField] private float subEffectCount;
    [SerializeField] private float delayAfterMain;

    [Header("CleavePatten")]
    [SerializeField] private string cleaveEffectName;
    [SerializeField] private float cleaveDamage;
    [SerializeField] private float cleaveAnimationDelayTime;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    protected override void Start()
    {
        AddPattern(new CleavePattern(cleaveEffectName, cleaveDamage, cleaveAnimationDelayTime, animator));
        AddPattern(new AxeSlashPattern(effectName, slashCount, slashDuration, slashDamage, slashAttackDelay, slashSpeed, animator, StageManager.instance.GetPlayerObject().transform));
        AddPattern(new GroundSpikePattern(mainEffectName, subEffectName, subSpikeDelayTime, mainSpikeDamage, mainSpikeDamage, subEffectRange, subEffectCount, delayAfterMain, animator));
        base.Start();
    }
}
