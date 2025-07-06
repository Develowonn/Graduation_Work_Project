using System.Collections;
using System.Collections.Generic;
using Unity.XR.OpenVR;
using UnityEngine;

public class BossMonster : Monster
{
    [SerializeField] private List<IBossPattern> patternList = new List<IBossPattern>();             // 패턴 리스트
    private int currentPatternIndex = 0;                                                            // 패턴 인덱스 ( 번갈아 사용하는 용도 )
    private bool isRandom = false;                                                                  // 패턴 랜덤 or 번갈아 사용 선택
    [SerializeField] protected float nextPatternTime;

    protected virtual void Start()
    {
        InGameManager.Instance.SetBossLogoActivity(true);

        StageManager.instance.BossMonsterHPBarInit(currentHp, maxHp);
        StartCoroutine(ProcessPatternLoop());
    }

	private void OnDisable()
	{
        InGameManager.Instance.SetBossLogoActivity(false);
	}

	/// <summary>
	/// 패턴 추가 함수
	/// </summary>
	/// <param name="pattern">추가할 패턴 코루틴</param>
	protected void AddPattern(IBossPattern pattern)
    {
        patternList.Add(pattern);
    }

    /// <summary>
    /// 패턴 루프 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator ProcessPatternLoop()
    {
        while (true)
        {
            var pattern = patternList[currentPatternIndex];
            isMoveStop = true;
            yield return StartCoroutine(pattern.Execute(this));
            isMoveStop = false;
            yield return new WaitForSeconds(nextPatternTime);

            if (!isRandom) currentPatternIndex = (currentPatternIndex + 1) % patternList.Count;
            else currentPatternIndex = Random.Range(0, patternList.Count);
        }
    }

    public float GetCurrentHp() { return currentHp; }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        StageManager.instance.BossMonsterHPBarInit(currentHp, maxHp);
    }
}
