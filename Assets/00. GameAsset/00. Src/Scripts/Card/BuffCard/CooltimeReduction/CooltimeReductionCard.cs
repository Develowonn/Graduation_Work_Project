// # System
using System;

// # Unity
using UnityEngine;

// # ETC
using Cysharp.Threading.Tasks;


public class CooltimeReductionCard : Card
{
    private GameObject  aura;

    [SerializeField]
    private float       cooolTimeReductionDuration;
    // 쿨타임 감소율
    [SerializeField]
    private float       coolTimeReductionPercent; 

    [Header("Offset")]
    [SerializeField]
    private Vector3     auraOffset;

    [Header("VFX")]
    [SerializeField]
    private GameObject  auraVFX;

    private Transform   player;

    public override void Execute()
    {
        player = InGameManager.Instance.GetPlayerObject().transform;

        ExecuteSkill().Forget();
    }

    private async UniTask ExecuteSkill()
    {
        SpawnAura();

        await ReduceSkillCooltime();
        Destroy(aura);
    }

    private void SpawnAura()
    {
        aura = Instantiate(auraVFX, player.transform.position + auraOffset, Quaternion.Euler(-90, 0, 0));
    }

    private async UniTask ReduceSkillCooltime()
    {
        // 쿨타임 감소율 적용 O
        PlayerSkillManager p = player.GetComponent<PlayerSkillManager>();
        p.SkillCoolTimeBuffTrigger(coolTimeReductionPercent, true);

        await UniTask.Delay(TimeSpan.FromSeconds(cooolTimeReductionDuration));

        p.SkillCoolTimeBuffTrigger(coolTimeReductionPercent, false);
        // 쿨타임 감소율 적용 X 
    }
}
