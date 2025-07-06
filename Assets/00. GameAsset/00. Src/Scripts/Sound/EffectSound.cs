using UnityEngine;

public class EffectSound : MonoBehaviour
{
    [SerializeField] private string soundName;

    private void OnEnable()
    {
        SoundManager.Instance.PlaySFX(soundName);
    }
}
