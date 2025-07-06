using UnityEngine;

public class EffectSound : MonoBehaviour
{
    [SerializeField] private string soundName;
    private bool isSpawn = false;

    private void OnEnable()
    {
        if(isSpawn) SoundManager.Instance.PlaySFX(soundName);
        else isSpawn = true;
    }
}
