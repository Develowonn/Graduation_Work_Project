using UnityEngine;

public enum Grade
{
    Legend,
    Epic,
    Rare,
    Common
}

//  등급 데이터 저장용도 SO
[CreateAssetMenu(fileName = "GradeSO", menuName = "Scriptable Objects/GradeSO")]
public class GradeSO : ScriptableObject
{
    public Grade grade;       // 등급
    public Color color;       // 색상
}
